using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ManagerExternalResources
{
    private InputHandler _inputHandler;
    private const string URL = "http://data.ikppbb.com/test-task-unity-data/pics/";
    private int amountSprite = 67;
    private int _numberLastLoadSprite = 0;
    private Coroutine _coroutine;

    public ManagerExternalResources(InputHandler inputHandler)
    {
        _inputHandler = inputHandler;
    }

    public void DownloadNextTexture2D(Action<Texture2D> response)
    {
        if (_coroutine == null && _numberLastLoadSprite < amountSprite)
        {
            _coroutine = _inputHandler.StartCoroutine(DownloadTextureFromServer(GetNextURL(), response));
        }
    }
    private string GetNextURL()
    {
        _numberLastLoadSprite++;
        return URL + _numberLastLoadSprite + ".jpg";
    }
        
    IEnumerator DownloadTextureFromServer(string url, Action<Texture2D> response)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);

        yield return request.SendWebRequest();

        if (!request.isHttpError && !request.isNetworkError)
        {
            response(DownloadHandlerTexture.GetContent(request));
        }
        else
        {
            Debug.LogErrorFormat("error request [{0}, {1}]", url, request.error);

            response(null);
        }
        request.Dispose();
        _coroutine = null;
    }

    public int GetLastNumberSprite()
    {
        return _numberLastLoadSprite;
    }

    
}