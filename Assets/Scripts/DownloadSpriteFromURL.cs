using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloadSpriteFromURL : MonoBehaviour
{
    public static List<RectTransform> ObjectsImage;
    public static List<Sprite> SpritesUploadedWithURL;
    
    [Header("URL для скачивания спрайтов")]
    [SerializeField] private string url = "http://data.ikppbb.com/test-task-unity-data/pics/";
    
    [Header("Ко-во спрайтов на сервере")]
    [SerializeField] private int amountSprite = 66;
    
    [Header("Префаб изображения")]
    [SerializeField] private RectTransform prefabImage;
    
    [Header("ScrollRect компонент")]
    [SerializeField] private ScrollRect scrollRect;
    
    [Header("Объект для размещения искачанных изображений")]
    [SerializeField] private RectTransform content;
    
    [Header("Панель для увеличения изображения")] [SerializeField]
    private GameObject maxPanel;

    private int _numberLastLoadSprite = 1;
    private string _finishURL;
    private Coroutine _coroutine;
    
    public void Start()
    {
        SpritesUploadedWithURL = new List<Sprite>();
        ObjectsImage = new List<RectTransform>();
    }

    private void Update()
    {
        if (scrollRect.normalizedPosition.y < 0.1f && _coroutine == null)
        {
            if(_numberLastLoadSprite > amountSprite) return;
            
            _coroutine = StartCoroutine(LoadTextureFromServer(GetNextURL(), CreateImage));
        }

        if (Input.GetKeyDown(KeyCode.Space) && _coroutine == null)
        {
            _coroutine = StartCoroutine(LoadTextureFromServer(GetNextURL(), CreateImage));
        }
    }

    private string GetNextURL()
    {
        return url + _numberLastLoadSprite + ".jpg";
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
            _coroutine = null;
        }

    private void CreateImage(Texture2D texture2D)
    {
            var sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
                    new Vector2(0.5f, 0.5f));
            SpritesUploadedWithURL.Add(sprite);
            var obj = Instantiate(prefabImage, this.transform.position, Quaternion.identity);
            ObjectsImage.Add(obj);
            obj.GetComponent<Image>().sprite = sprite;
            obj.transform.SetParent(content.transform);
            obj.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
            obj.name = "Image_" + _numberLastLoadSprite;
            obj.GetComponent<ImageHandler>().MaxPanel = maxPanel;
            Debug.Log("Create sprite " + _numberLastLoadSprite);
            _numberLastLoadSprite++;
    }
}
