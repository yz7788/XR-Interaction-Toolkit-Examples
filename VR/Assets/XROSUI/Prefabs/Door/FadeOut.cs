﻿/*
 * Idea and code from link:
 * https://www.youtube.com/watch?v=Oadq-IrOazg
 * https://www.youtube.com/watch?v=JS4k_lwmZHk
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    [SerializeField] private Animator animationController;
    Camera m_MainCamera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            animationController.SetBool("fadeOut", true);
            Debug.Log("fadeOut true");

        }
    }

    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Heart"))
        {
            animationController.SetBool("fadeOut", false);
            Debug.Log("fadeOut false");
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
