using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class LaserLengthChange : MonoBehaviour
{

    XRGrabInteractable m_GrabInteractable;
    XRRayInteractor LaserFromRay;
    // MeshRenderer m_MeshRenderer;
    public GameObject LaserFrom;
    public GameObject LaserFromEmitter;
    public GameObject LaserReceiving;
    private bool grabbed;
    private float priorDistance;
    private Quaternion priorDirection;

    void OnEnable()
    {
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
        m_GrabInteractable.onSelectEnter.AddListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.AddListener(OnReleased);
    }

    void OnGrabbed(){
        this.grabbed=true;
        this.priorDirection=this.LaserFromEmitter.transform.rotation;
        priorDistance=Vector3.Distance(LaserFrom.transform.position,LaserReceiving.transform.position);
    }

    void OnReleased(){
        this.grabbed=false;
        this.LaserFromEmitter.transform.rotation=this.priorDirection;

    }

    // Start is called before the first frame update
    void Start()
    {
        this.grabbed=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.grabbed){
            float newDistance=Vector3.Distance(LaserFrom.transform.position,LaserReceiving.transform.position);
            LaserFromRay=LaserFrom.GetComponent<XRRayInteractor>();
            LaserFromRay.maxRaycastDistance+=(newDistance-priorDistance);
            Vector3 direction=LaserReceiving.transform.position-LaserFrom.transform.position;
            LaserFromEmitter.transform.forward=direction;
            priorDistance=newDistance;
        }
    }
}
