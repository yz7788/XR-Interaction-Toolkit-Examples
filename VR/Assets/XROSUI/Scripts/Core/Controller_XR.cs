using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_XR : MonoBehaviour
{
    Camera XrCamera;
    
    // Start is called before the first frame update
    void Start()
    {
        if(XrCamera)
        {
            XrCamera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterCamera(Camera camera)
    {
        XrCamera = camera;
    }
}
