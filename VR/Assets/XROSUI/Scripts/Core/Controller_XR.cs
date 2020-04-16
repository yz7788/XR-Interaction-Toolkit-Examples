using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_XR : MonoBehaviour
{
    GameObject XRRig;
    Camera XrCamera;

    // Start is called before the first frame update
    void Start()
    {
        //These are fail safes in case no one registered and no one dragged
        if (!XrCamera)
        {
            //Dev.LogError("No XR Camera registered, attempting to substitute with main camera");
            XrCamera = Camera.main;
        }
        if (!XRRig)
        {
            //Dev.LogError("No XRRIG registered, attempting to substitute with XRRIG_XROS");
            XRRig = GameObject.Find("XRRig_XROS");
            if(!XRRig)
            {
                Dev.LogError("Cannot find XRRIG_XROS");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Camera GetCamera()
    {
        return XrCamera;
    }

    public GameObject GetXRRig()
    {
        return XRRig;
    }

    public void RegisterCamera(Camera camera)
    {
        XrCamera = camera;
    }

    public void RegisterXRRig(GameObject go)
    {
        XRRig = go;
    }
}
