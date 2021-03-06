﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
public class MinimizeSwitch : MonoBehaviour
{
    XRGrabInteractable m_GrabInteractable;
    MeshRenderer m_MeshRenderer;

    static Color m_UnityMagenta = new Color(0.929f, 0.094f, 0.278f);
    static Color m_UnityCyan = new Color(0.019f, 0.733f, 0.827f);
    static Color m_transparent = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    bool m_Held = false;
    public bool bMinimize;
    public List<GameObject> MinimizeList;
    void OnEnable()
    {
        m_GrabInteractable = GetComponent<XRGrabInteractable>();
        m_MeshRenderer = GetComponent<MeshRenderer>();

        m_GrabInteractable.onFirstHoverEnter.AddListener(OnHoverEnter);
        m_GrabInteractable.onLastHoverExit.AddListener(OnHoverExit);
        m_GrabInteractable.onSelectEnter.AddListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.AddListener(OnReleased);
        m_GrabInteractable.onActivate.AddListener(OnActivated);

        Minimize();
    }


    private void OnDisable()
    {
        m_GrabInteractable.onFirstHoverEnter.RemoveListener(OnHoverEnter);
        m_GrabInteractable.onLastHoverExit.RemoveListener(OnHoverExit);
        m_GrabInteractable.onSelectEnter.RemoveListener(OnGrabbed);
        m_GrabInteractable.onSelectExit.RemoveListener(OnReleased);
        m_GrabInteractable.onActivate.RemoveListener(OnActivated);
    }

    // Start is called before the first frame update
    void Start()
    {

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
            //print("grabbing " + Time.time);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Minimize();
        }
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
    void OnActivated(XRBaseInteractor obj)
    {
        Minimize();
    }

    private void Minimize()
    {
        bMinimize = !bMinimize;
        foreach (GameObject go in this.MinimizeList)
        {
            go.SetActive(bMinimize);
        }
    }
}
