using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllerResetButton : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject m_controller;
    public GameObject m_Button;
    public GameObject m_Emitter;
    MeshRenderer m_Renderer;
    XRGrabInteractable m_XRGrabInteractable;
    XRRayInteractor m_XRRayInteractor;
    void Start()
    {
        m_Renderer = m_Button.GetComponent<MeshRenderer>();
        m_XRGrabInteractable = m_Button.GetComponent<XRGrabInteractable>();
        m_XRRayInteractor = m_controller.GetComponent<XRRayInteractor>();
        m_XRGrabInteractable.onSelectEnter.AddListener(OnGrabbed);
    }

    // Update is called once per frame
    void Update()
    {
        m_Button.transform.position = m_controller.transform.position - m_controller.transform.up * (0.03f) - m_controller.transform.forward * (0.002f);
        if (Vector3.Dot(m_controller.transform.up, Vector3.up) < -0.5f)
        {
            // Button.SetActive(true);
            m_Renderer.enabled = true;
            m_XRGrabInteractable.enabled = true;
            // print("Sekai:" + Vector3.up+"Rimokon:" +Controller.transform.up);
        }
        else
        {
            // Button.SetActive(false);
            m_Renderer.enabled = false;
            m_XRGrabInteractable.enabled = false;
        }
    }

    void OnGrabbed(XRBaseInteractor obj)
    {
        this.m_Emitter.transform.forward = m_controller.transform.forward;
        m_XRRayInteractor.maxRaycastDistance = 10;
    }
}
