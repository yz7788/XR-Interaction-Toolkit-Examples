using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRHeadphone : VREquipment
{
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
 }
