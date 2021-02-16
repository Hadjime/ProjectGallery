﻿using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadSpriteFromURL : MonoBehaviour
{
    [SerializeField] private string url = "http://data.ikppbb.com/test-task-unity-data/pics/";
    [SerializeField] private RectTransform _prefabImage;

    private List<RectTransform> _prefabs;
    private List<Sprite> _sprites;
    private SpriteRenderer _spriteRenderer;
    private int _numberSprite = 66;
    public void Start()
    {
        _sprites = new List<Sprite>();
        _prefabs = new List<RectTransform>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //StartCoroutine(LoadTextureFromServer(url, SetTexture));
        for (int i = 1; i <= _numberSprite; i++)
        {
            var newURL = url + i + ".jpg";
            StartCoroutine(LoadTextureFromServer(newURL,(Texture2D response) =>
            {
                var sprite = Sprite.Create(response, new Rect(0, 0, response.width, response.height),
                    new Vector2(0.5f, 0.5f));
                _sprites.Add(sprite);
                //_spriteRenderer.sprite = sprite;
                Debug.Log("Loaded sprite " + i);
                var obj = Instantiate(_prefabImage, this.transform.position, Quaternion.identity);
                _prefabs.Add(obj);
                obj.GetComponent<Image>().sprite = sprite;
                obj.transform.SetParent(this.transform);
                obj.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
                obj.name = "Image_" + i;
            }));
        }


    }

    IEnumerator LoadTextureFromServer(string url, Action<Texture2D> response)
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
        }

    public void SetTexture(Texture2D texture2D)
    {
        Texture2D tex = texture2D;
        _spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }

    [ContextMenu("SetScale")]
    public void SetScale()
    {
        foreach (var prefab in _prefabs)
        {
            prefab.GetComponent<RectTransform>().localScale = Vector3.one;
        }
    }
}
