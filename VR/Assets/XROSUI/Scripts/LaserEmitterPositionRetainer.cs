using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class LaserEmitterPositionRetainer : MonoBehaviour
{

    XRGrabInteractable m_GrabInteractable;
    MeshRenderer m_MeshRenderer;
    
    GameObject leftBaseController;
    GameObject rightBaseController;

    Vector3 priorDirection;

    static Color m_UnityMagenta = new Color(0.929f, 0.094f, 0.278f);
    static Color m_UnityCyan = new Color(0.019f, 0.733f, 0.827f);

    bool m_Held = false;

    void OnEnable()
    {
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        
        m_GrabInteractable.onFirstHoverEnter.AddListener(OnHoverEnter);
        m_GrabInteractable.onLastHoverExit.AddListener(OnHoverExit);
        m_GrabInteractable.onSelectEnter.AddListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.AddListener(OnReleased);
    }

    
    private void OnDisable()
    {
        m_GrabInteractable.onFirstHoverEnter.RemoveListener(OnHoverEnter);
        m_GrabInteractable.onLastHoverExit.RemoveListener(OnHoverExit);
        m_GrabInteractable.onSelectEnter.RemoveListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.RemoveListener(OnReleased);
    }

    private void OnGrabbed(XRBaseInteractor obj)
    {
        m_MeshRenderer.material.color = m_UnityCyan;
        m_Held = true;
        this.priorDirection=this.rightBaseController.transform.forward;
        // InvokeRepeating("positionRetainer",0,0.005f);//Works
    }
    
    void OnReleased(XRBaseInteractor obj)
    {
        m_MeshRenderer.material.color = Color.white;
        m_Held = false;
        // CancelInvoke();
    }
    

    void OnHoverExit(XRBaseInteractor obj)
    {
        if (!m_Held)
        {
            m_MeshRenderer.material.color = Color.white;
        }
    }

    void OnHoverEnter(XRBaseInteractor obj)
    {
        if (!m_Held)
        {
            m_MeshRenderer.material.color = m_UnityMagenta;
        }
    }


    void positionRetainer(){
        if(m_Held)
        {
            // print("grabbing " + Time.time);
            transform.position=leftBaseController.transform.position + leftBaseController.transform.forward*0.06f;

            Vector3 newDirection=rightBaseController.transform.forward;
            Vector3 normalVector=Vector3.Cross(newDirection,this.priorDirection);
            transform.RotateAround(transform.position,normalVector,-Vector3.Angle(priorDirection,newDirection));
            priorDirection=newDirection;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        this.leftBaseController=GameObject.Find("LeftBaseController");
        this.rightBaseController=GameObject.Find("RightBaseController");
        
    }

    // Update is called once per frame

    private void Update()
    {
        if(m_Held) 
        {
            // print("grabbing " + Time.time);
            this.positionRetainer();
        }

        

    }
}
