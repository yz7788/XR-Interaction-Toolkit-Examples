using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerResetButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Controller;
    public GameObject Button;
    public GameObject Emitter;
    MeshRenderer mr;
    XRGrabInteractable xrgi;
    XRRayInteractor xrri;
    void Start()
    {
        mr=Button.GetComponent<MeshRenderer>();
        xrgi=Button.GetComponent<XRGrabInteractable>();
        xrri=Controller.GetComponent<XRRayInteractor>();
        xrgi.onSelectEnter.AddListener(OnGrabbed);
    }

    // Update is called once per frame
    void Update()
    {
        Button.transform.position=Controller.transform.position-Controller.transform.up*(0.03f)-Controller.transform.forward*(0.002f);
        if(Vector3.Dot(Controller.transform.up,Vector3.up)<-0.5f){
            // Button.SetActive(true);
            mr.enabled = true;
            xrgi.enabled=true;
            // print("Sekai:" + Vector3.up+"Rimokon:" +Controller.transform.up);
        } else {
            // Button.SetActive(false);
            mr.enabled = false;
            xrgi.enabled=false;
        }
    }
    
    void OnGrabbed(XRBaseInteractor obj){
        this.Emitter.transform.forward=Controller.transform.forward;
        xrri.maxRaycastDistance=10;
    }
}
