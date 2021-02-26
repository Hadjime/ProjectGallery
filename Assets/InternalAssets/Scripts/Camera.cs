using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera
{
    static Dictionary<int, Camera> _cameras = new Dictionary<int, Camera>();
    static object _lock = new object();
 
    private Camera()
    {
        HardwareId = (int)Random.Range(0.0f, 10.0f);
    }
 
    public static Camera GetCamera(int cameraCode)
    {
        lock (_lock)
        {
            if (!_cameras.ContainsKey(cameraCode)) _cameras.Add(cameraCode, new Camera());
        }
        return _cameras[cameraCode];
    }
 
    public int HardwareId { get; private set; }
}
