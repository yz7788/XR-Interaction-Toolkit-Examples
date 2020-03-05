using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Visual : MonoBehaviour
{
    public Light directionalLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha9))
        {
            AdjustLight(0.5f);
        }
    }

    public void AdjustLight(float f)
    {
        f = f * 100;
        f += directionalLight.intensity;
        Dev.Log("New Light: " + f);
        SetLight(f);

    }
    public void SetLight(float f)
    {
        if (f > 1)
        {
            f = 1;
        }
        else if (f < 0)
        {
            f = 0;
        }

        directionalLight.intensity = f;
    }
}
