using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_FollowXRRigV2 : MonoBehaviour
{
    public GameObject GO_XRRigCamera;
    public bool bFollows;
    public float offset_x = 0.5f;
    public float offset_y = 0f;
    public float offset_z = 0f;
    public float UpdateFrequencyInSeconds = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        //Canvas.worldCamera = GameObject.Find("XRRig_XROS").GetComponent<UnityEngine.XR.Interaction.Toolkit.XRRig>().cameraGameObject.GetComponent<Camera>();        
        GO_XRRigCamera = Camera.main.gameObject;

        FollowCamera();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (bFollows)
        {
            FollowCamera();
        }
    }

    void FollowCamera()
    {

        this.transform.position = GO_XRRigCamera.transform.position + new Vector3(offset_x, offset_y, offset_z);
    }
}
