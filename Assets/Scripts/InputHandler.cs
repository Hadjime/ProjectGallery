using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{

    [SerializeField] private SpawnObject spawnObject;

    private void Update()
    {
        if (spawnObject.scrollRect.normalizedPosition.y < 0.1f)
        {
            spawnObject.UploadImage();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnObject.UploadImage();
        }
    }

    
}
