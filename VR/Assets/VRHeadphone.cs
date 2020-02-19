using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRHeadphone : VREquipment
{
    public GameObject fakeSocket;
    public void Start()
    {

//        fakeSocket = this.transform.parent.gameObject;
    }
    public override void AlternateFunction()
    {
        print(this.name);
        mainMenu.SetActive(false);
        audioMenu.SetActive(true);

    }

    void OnReleased(XRBaseInteractor obj)
    {
        m_MeshRenderer.material.color = Color.white;
        m_Held = false;

    }

    float lastHeldTime;
    private void Update()
    {
        if(m_Held)
        {
            lastHeldTime = Time.time;            
        }
        else if(!m_Held && Time.time > lastHeldTime + 2f)
        {
            this.transform.position = fakeSocket.transform.position;
            this.transform.SetParent(fakeSocket.transform);
        }
    }
}
