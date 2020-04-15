
/*
 * Idea and code used from link:
 * https://www.youtube.com/watch?v=JS4k_lwmZHk
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRightDoorAnimation : MonoBehaviour
{
    public Animator animationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            animationController.SetBool("openRightDoor", true);
            Debug.Log("openRightDoor");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            animationController.SetBool("openRightDoor", false);
        }
    }
}
