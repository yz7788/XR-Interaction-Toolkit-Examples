using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class XRKey : MonoBehaviour
{
    public KeyboardController keyboardController;
    public Text myText;
    Color transparent = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    XRGrabInteractable m_GrabInteractable;
    MeshRenderer m_MeshRenderer;
    static Color m_UnityMagenta = new Color(0.929f, 0.094f, 0.278f);
    static Color m_UnityCyan = new Color(0.019f, 0.733f, 0.827f);
    bool m_Held = false;

    public void Setup(string s, KeyboardController kc)
    {
        this.keyboardController = kc;
        this.name = "Key: " + s;
        myText.text = s;
    }

    void OnEnable()
    {
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
        m_MeshRenderer = GetComponent<MeshRenderer>();
        m_MeshRenderer.material.color = transparent;
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
    }

    void OnReleased(XRBaseInteractor obj)
    {
        m_MeshRenderer.material.color = Color.white;
        m_Held = false;
    }
    private void Update()
    {
        if (m_Held)
        {
            /*print("grabbing " + Time.time);*/
        }
    }

    void OnHoverExit(XRBaseInteractor obj)
    {
        if (!m_Held)
        {
            m_MeshRenderer.material.color = transparent;
        }
    }

    void OnHoverEnter(XRBaseInteractor obj)
    {
        /*if (!m_Held & keyboardController.getWaiting() == false)
        {
            *//*            if (myKey == "DEL")
                        {
                            XROSInput.RemoveInput();
                            return;
                        }*//*
            keyboardController.wait();
            keyboardController.SetWaiting();
            keyboardController.RegisterInput(myText.text);
            XROSInput.AddInput(myText.text);
            m_MeshRenderer.material.color = m_UnityMagenta;
        }*/
    }
    
    public void OnKeyClicked()
    {
        /*            keyboardController.wait();
                    keyboardController.SetWaiting();*/
        print("called");
        keyboardController.RegisterInput(myText.text);
        XROSInput.AddInput(myText.text);
        m_MeshRenderer.material.color = m_UnityMagenta;
        
    }
}
