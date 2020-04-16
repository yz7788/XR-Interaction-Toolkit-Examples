using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_RotateAroundXRRig : MonoBehaviour
{
    public float degreeToShow = 45f;
    public float degreesToRotate = 25f;
    public float DistanceAwayFromUser = 0.5f;
    public Vector3 Offset;
    Transform XRCameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        //XRCameraTransform = Core.Ins.XRManager.GetCamera().transform;
        XRCameraTransform = Camera.main.transform;
        //this.transform.position = XRCameraTransform.position + XRCameraTransform.forward * 0.5f;
        //this.transform.RotateAround(XRCameraTransform.position, Vector3.up, 20f);
        //this.transform.LookAt(XRCameraTransform);
    }

    // Update is called once per frame
    void Update()
    {
        float currentDegree = Vector3.Angle(XRCameraTransform.forward, -this.transform.forward);
        //print(currentDegree);
        if (currentDegree < degreeToShow)
        {
            //print("no need to change");
        }
        else
        {
            //print("need to change ");
            //Offset doesn't really work since it will rotate weird
            this.transform.position = XRCameraTransform.position + XRCameraTransform.forward * DistanceAwayFromUser + Offset;
            
            this.transform.RotateAround(XRCameraTransform.forward, Vector3.up, degreesToRotate);

            this.transform.LookAt(XRCameraTransform);
        }
    }
}
