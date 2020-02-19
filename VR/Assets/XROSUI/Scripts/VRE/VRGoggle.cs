using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRGoggle : VREquipment
{
    

    public override void AlternateFunction()
    {
        print(this.name);
        mainMenu.SetActive(false);
        goggleMenu.SetActive(true);
    }
}
