/*
 * Idea and code used from link:
 * https://www.youtube.com/watch?v=JS4k_lwmZHk
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLeftDoorAnimation : MonoBehaviour
{
    public Animator animationController;
    public Animator animationController2;
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Heart"))
        VREquipment vre = other.GetComponent<VREquipment>();
        if (vre)
        {
            animationController.SetBool("openLeftDoor", true);
            animationController2.SetBool("openRightDoor", true);
            Core.Ins.ScenarioManager.SetFlag("OpenDoor", true);//tell the Core you are openning the door.
            // Debug.Log("openLeftDoor");
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animationController.SetBool("openLeftDoor", true);
            animationController2.SetBool("openRightDoor", true);
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            animationController.SetBool("openLeftDoor", false);
            animationController2.SetBool("openRightDoor", false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            animationController.SetBool("openLeftDoor", false);
        }
    }
}
