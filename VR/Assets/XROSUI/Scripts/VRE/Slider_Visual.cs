using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_Visual : MonoBehaviour
{
    
    Controller_Visual light;
    public float light_value = 0.0001f;
    // Start is called before the first frame update
    void Start()
    {
        light = Core.Ins.VisualManager;
    }

    public void SetLight(float f)
    {
        light.SetLight(f);
        light_value = f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
