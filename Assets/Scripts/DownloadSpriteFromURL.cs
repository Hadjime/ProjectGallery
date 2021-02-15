using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DownloadSpriteFromURL : MonoBehaviour
{
    public string url = "http://data.ikppbb.com/test-task-unity-data/pics/2.jpg";


    private Texture2D _tex;
    private SpriteRenderer _spriteRenderer;
    public void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tex = new Texture2D(1, 1);
        StartCoroutine(LoadTextureFromServer(url, SetTexture));
        
        /*StartCoroutine(LoadTextureFromServer(url,(Texture2D response) =>
        {
            _tex = response;
        }));*/
        
        
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
        tex.Apply();
        _spriteRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }
}
