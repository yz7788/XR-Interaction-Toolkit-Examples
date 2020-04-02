using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Maintained by Powen & Sophie
//https://youtu.be/MDvPNNgIu7k

public class Controller_Visual : MonoBehaviour
{
    //Typically the directionallight in the scene (as it represents the sun)
    public Light m_Light;
    public bool bLightExists = false;
    
    public Text text;
    Text Text_brightnessValue;
    float m_LightIntensity;

    float minValue = 0;
    float maxValue = 1;
    //public float lightIntensityAdjuster = 100;

    // Start is called before the first frame update
    void Start()
    {
        bLightExists = CheckIfLightExists();

        GameObject text = GameObject.Find("Text_brightnessValue");

        if (text != null)
        {
            Text_brightnessValue = text.GetComponent<Text>();
            if (Text_brightnessValue != null)
            {
                Text_brightnessValue.text = "Brightness:" + ((int)(m_LightIntensity * 100f)).ToString() + "%";
            }
            else { Debug.LogError("[" + text.name + "]- Dose not contain a Text component"); }
        }
        else { Debug.LogError("Could not find Text_brightnessValue"); };
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
            m_LightIntensity = m_Light.intensity;
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

    //float changeRate2 = 0.11f;
    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AdjustLight(0.5f);
        }
        */
    }

    public void AdjustBrightness(float f)
    {
        //f = f * lightIntensityAdjuster;
        if (bLightExists)
        {
            f += m_Light.intensity;
            //Dev.Log("New Light: " + f);
            SetLight(f);
        }
    }

    public float GetBrightness()
    {
        return m_Light.intensity;
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
            //Debug.Log("bright"+f);
            //Text_brightnessValue.text = "Brightness:" + ((int)(f * 100f)).ToString() + "%";
        }
    }
}
