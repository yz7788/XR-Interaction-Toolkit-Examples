using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_LookAtXRRig : MonoBehaviour
{
    Transform XRCameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        XRCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(XRCameraTransform);
    }
}
