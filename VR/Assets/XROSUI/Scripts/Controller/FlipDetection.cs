using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class FlipDetection : MonoBehaviour
{
    public UnityEvent EnterFlipPosition;
    public UnityEvent ExitFlipPosition; 
    public float Tolerance = 0.3f;
    bool m_IsFlipped;

    // Start is called before the first frame update
    void Start()
    {
        this.m_IsFlipped=false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Dot(transform.up,Vector3.up)<(Tolerance-1)){
            if(!m_IsFlipped){
                this.EnterFlipPosition.Invoke();
                this.m_IsFlipped=true;
                print("flipped");
            }
        } else {
            if(m_IsFlipped){
                this.ExitFlipPosition.Invoke();
                this.m_IsFlipped=false;
                print("NNflipped");
            }
        }
    }
}
