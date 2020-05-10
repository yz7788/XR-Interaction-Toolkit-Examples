using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
[RequireComponent(typeof(Canvas))]
public class Tool_GetWorldSpaceCamera : MonoBehaviour
{
    public Canvas Canvas;

    // Start is called before the first frame update
    void Start()
    {
        SetCamera();
    }

    private void SetCamera()
    {
        //Canvas.worldCamera = GameObject.Find("XRRig_XROS").GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRig>().cameraGameObject.GetComponent<Camera>();
        if (!Canvas)
        {
            Canvas = this.GetComponent<Canvas>();
        }

        if (!Canvas.worldCamera)
        {
            Canvas.worldCamera = Camera.main;
        }
    }
    private void Update()
    {

    }
}
