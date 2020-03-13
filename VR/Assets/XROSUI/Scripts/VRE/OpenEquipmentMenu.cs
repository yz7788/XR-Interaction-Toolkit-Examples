using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenEquipmentMenu : MonoBehaviour
{
    public Controller_GameMenu gameMenu;
    public void Start()
    {
        gameMenu = GameObject.Find("UIParent").GetComponent<Controller_GameMenu>();
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.name);
        if (other.CompareTag("AlternateFunction"))
        {

            if(other.GetComponent<VREquipment>())
            {
                //Dev.Log("Other: " + other.name);
                Debug.Log("triggerAlter");
                VREquipment vre = other.GetComponent<VREquipment>();
                //vre.AlternateFunction();
                
                switch (vre.name)
                {
                    case "Headphone":
                        gameMenu.OpenMenu("Menu_Audio");
                        Debug.Log("Menu_Audio");
                        break;
                    case "Goggle":
                        gameMenu.OpenMenu("Menu_Visual");
                        Debug.Log("Menu_Visual");
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
