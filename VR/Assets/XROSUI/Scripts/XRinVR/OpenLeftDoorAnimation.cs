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
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            animationController.SetBool("openLeftDoor", true);
            Core.Ins.ScenarioManager.SetFlag("OpenDoor",true);//tell the Core you are openning the door.
            // Debug.Log("openLeftDoor");
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
