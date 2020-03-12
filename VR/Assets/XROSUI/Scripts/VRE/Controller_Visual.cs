﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Maintained by Powen & Sophie

//https://youtu.be/MDvPNNgIu7k

public class Controller_Visual : MonoBehaviour
{
    //Typicall the directionallightin the scene (as it represents the sun)
    public Light m_Light;
    public bool bLightExists = false;
    public float GammaCorrection;

    float minValue = 0;
    float maxValue = 1;
    //
    //public float lightIntensityAdjuster = 100;

    // Start is called before the first frame update
    void Start()
    {
        bLightExists = CheckIfLightExists();
    }
    public void OnEnable()
    {
        bLightExists = CheckIfLightExists();
    }
    public void OnDisable()
    {
        bLightExists = false;
    }

    private bool CheckIfLightExists()
    {
        if (m_Light)
        {
            return true;
        }

        GameObject go = GameObject.Find("Directional Light");
        if (go && go.GetComponent<Light>())
        {
            Dev.Log("[Hack] Assigning Directional Light as light");
            m_Light = go.GetComponent<Light>();
            return true;
        }

        Dev.LogError("Controller_Visual is missing its light");
        return false;
    }

    float changeRate2 = 0.11f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AdjustLight(0.5f);
        }

        if (Input.GetKey(KeyCode.T))
        {
            //"You wouldn't want them to be able to turn it all the way up, but perhaps somewhere between 10% and 40% might be a good range for a general "brightness" slider."
            GammaCorrection += changeRate2;
            print(GammaCorrection);
            RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1.0f);
        }
        if (Input.GetKey(KeyCode.G))
        {
            GammaCorrection -= changeRate2;
            print(GammaCorrection);
            RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1.0f);
        }

    }

    public void AdjustLight(float f)
    {
        //f = f * lightIntensityAdjuster;
        if (bLightExists)
        {
            f += m_Light.intensity;
            //Dev.Log("New Light: " + f);
            SetLight(f);
        }
    }
    public void SetLight(float f)
    {
        if (f > maxValue)
        {
            f = maxValue;
        }
        else if (f < minValue)
        {
            f = minValue;
        }

        if (bLightExists)
        {
            m_Light.intensity = f;
        }
    }
}