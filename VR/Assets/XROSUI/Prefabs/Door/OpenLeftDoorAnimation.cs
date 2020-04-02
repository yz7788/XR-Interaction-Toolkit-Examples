using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLeftDoorAnimation : MonoBehaviour
{

    [SerializeField] private Animator animationController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            animationController.SetBool("openLeftDoor", true);
            Debug.Log("openLeftDoor");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            animationController.SetBool("openLeftDoor", false);
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
