using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRGoggle : VREquipment
{   
    public override void AlternateFunction()
    {
        Dev.Log("Alternate Function: " + this.name);
        mainMenu.SetActive(false);
        settingMenu.SetActive(false);
        audioMenu.SetActive(false);
        goggleMenu.SetActive(true);
    }
}
