using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script is used on Alt Node so that any VRE in contact with Alt Node can open the menu associated with that VRE
/// </summary>
public class OpenEquipmentMenu : MonoBehaviour
{
    //VREquipment vre;
    void OnTriggerEnter(Collider other)
    {
        VREquipment vre = other.GetComponent<VREquipment>();
        if (vre)
        {
            Core.Ins.SystemMenu.OpenMenu(vre.menuTypes);
        }
    }
}
