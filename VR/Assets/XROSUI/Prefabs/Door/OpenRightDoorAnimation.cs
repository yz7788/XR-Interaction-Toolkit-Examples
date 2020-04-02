using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRightDoorAnimation : MonoBehaviour
{
    [SerializeField] private Animator animationController;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
