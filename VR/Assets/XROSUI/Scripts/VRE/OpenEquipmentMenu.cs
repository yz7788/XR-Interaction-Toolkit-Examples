using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenEquipmentMenu : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.CompareTag("AlternateFunction"))
        {

            if(other.GetComponent<VREquipment>())
            {
                VREquipment vre = other.GetComponent<VREquipment>();
                vre.AlternateFunction();
            }
        }
    }
}
