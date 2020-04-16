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
        //print(other.name);
        if (other.CompareTag("AlternateFunction"))
        {
            if (other.name == " ")
            {
                gameMenu.OpenMenu("Menu_General");
                Debug.Log("Menu_General");
            }
            
            if (other.GetComponent<VREquipment>())
            {
                VREquipment vre = other.GetComponent<VREquipment>();
                gameMenu.OpenMenu(vre.menuTypes);
            }                        
        }
    }
}
