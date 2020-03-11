using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.UI;


public class RayControllerHidingLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    XRInteractorLineVisual xRInteractorLineVisual;
    public LaserLengthChange grabbedTarget;
    public LaserTracking laserTracker;
    bool grabbing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        this.lineRenderer=this.GetComponent<LineRenderer>();
        this.xRInteractorLineVisual=this.GetComponent<XRInteractorLineVisual>();
        this.grabbing=false;
    }

    public void OnGrab(/*XRBaseInteractor obj*/){
        if(!this.grabbing){
            this.grabbing=true;
        }
    }

    public void OnRelease(/*XRBaseInteractor obj*/){
        if(this.grabbing){
            this.grabbing=false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(this.grabbing&&!grabbedTarget.grabbed&&!laserTracker.m_Held){
            // print("onGrab grabbed="+(grabbedTarget.grabbed? "true":"false"));
            // this.lineRenderer.enabled=false;
            if(this.xRInteractorLineVisual.enabled){
                this.xRInteractorLineVisual.enabled=false;
            }
        } else {
            if(!this.xRInteractorLineVisual.enabled){
                this.xRInteractorLineVisual.enabled=true;
            }
        }
    }
}
