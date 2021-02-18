using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public static List<RectTransform> ObjectsImage;
    public static List<Sprite> SpritesUploadedWithURL;
    
    [Header("Префаб изображения")]
    [SerializeField] private RectTransform prefabImage;
    
    [Header("ScrollRect компонент")]
    [SerializeField] private ScrollRect scrollRect;
    
    [Header("Объект для размещения искачанных изображений")]
    [SerializeField] private RectTransform content;
    
    [Header("Панель для увеличения изображения")] 
    [SerializeField] private GameObject maxPanel;
    
    private ManagerExternalResources _managerExternalResources;

    private void Awake()
    {
        ObjectsImage = new List<RectTransform>();
        SpritesUploadedWithURL = new List<Sprite>();
    }

    public void Start()
    {
        _managerExternalResources = new ManagerExternalResources(this);
    }

    private void Update()
    {
        if (scrollRect.normalizedPosition.y < 0.1f)
        {
            _managerExternalResources.DownloadNextTexture2D(CreateImage);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _managerExternalResources.DownloadNextTexture2D(CreateImage);
        }
    }

    private void CreateImage(Texture2D texture2D)
    {
        if (texture2D == null) return;
        
        var sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
            new Vector2(0.5f, 0.5f));
        SpritesUploadedWithURL.Add(sprite);
        var obj = Instantiate(prefabImage, transform.position, Quaternion.identity);
        ObjectsImage.Add(obj);
        obj.GetComponent<Image>().sprite = sprite;
        obj.transform.SetParent(content.transform);
        obj.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
        obj.name = "Image_" + _managerExternalResources.GetLastNumberSprite();
        obj.GetComponent<ImageHandler>().MaxPanel = maxPanel;
        Debug.Log("Create sprite " + _managerExternalResources.GetLastNumberSprite() );
    }
}
