using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    private GameObject _panelForSelectedSprite;
    private Sprite _currentSprite;
    private Image _image;
    private void Awake()
    {

    }

    void Start()
    {
        _panelForSelectedSprite = FindInActiveObjectByName("PanelForSelectedSprite");
        if (_panelForSelectedSprite == null )
        {
            Debug.Log("PanelForSelectedSprite not found");
        }
        else
        {
            _image = _panelForSelectedSprite.transform.Find("ImageMaxSize").GetComponent<Image>();
        }

        _currentSprite = GetComponent<Image>().sprite;
    }


    void Update()
    {
        
    }

    public void Select()
    {
        _image.sprite = _currentSprite;
        _panelForSelectedSprite.SetActive(true);
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    
    public void Undo()
    {
        _panelForSelectedSprite.SetActive(false);
        Screen.orientation = ScreenOrientation.Portrait;
    }
    
    GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
