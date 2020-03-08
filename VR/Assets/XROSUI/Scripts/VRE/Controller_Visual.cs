using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Maintained by Powen & Sophie

public class Controller_Visual : MonoBehaviour
{
    //Typicall the directionallightin the scene (as it represents the sun)
    public Light m_Light;

    //
    //public float lightIntensityAdjuster = 100;

    // Start is called before the first frame update
    void Start()
    {
        CheckIfLightExists();
    }

    private bool CheckIfLightExists()
    {
        if (m_Light)
        {
            return true;
        }
        Dev.LogError("Controller_Visual is missing its light");
        return false;
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
        //f = f * lightIntensityAdjuster;
        if (CheckIfLightExists())
        {
            f += m_Light.intensity;
            Dev.Log("New Light: " + f);
            SetLight(f);
        }
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

        if (CheckIfLightExists())
        {
            m_Light.intensity = f;
        }
    }
}
