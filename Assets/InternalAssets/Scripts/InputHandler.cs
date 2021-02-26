using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityNightPool;

public class InputHandler : MonoBehaviour
{

    [SerializeField] private SpawnObject spawnObject;

    private void Update()
    {
        if (spawnObject.scrollRect.normalizedPosition.y < 0.1f)
        {
            spawnObject.Spawn();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnObject.Spawn();
            var obj = PoolManager.Get(1);
            var corutine = StartCoroutine(WaitAndReturn(obj));
        }

        IEnumerator WaitAndReturn(PoolObject poolObject)
        {
            yield return new WaitForSeconds(2.0f);
            poolObject.Return();
        }
    }

    
}
