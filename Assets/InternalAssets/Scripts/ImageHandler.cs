using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageHandler : MonoBehaviour
{
    public GameObject MaxPanel { get; set; }
    
    private Sprite _currentSprite;
    
    void Start()
    {
        _currentSprite = GetComponent<Image>().sprite;
    }
    
    public void Select()
    {
        MaxPanel.GetComponent<Image>().sprite = _currentSprite;
        MaxPanel.SetActive(true);
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
}
