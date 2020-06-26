using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
//https://youtu.be/MDvPNNgIu7k
//https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@7.1/manual/Volumes.html
//https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@7.1/manual/Post-Processing-Lift-Gamma-Gain.html
//https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@7.1/manual/Post-Processing-Color-Adjustments.html
/*Post-processing in URP for VR
In VR apps and games, certain post-processing effects can cause nausea and disorientation. 
To reduce motion sickness in fast-paced or high-speed apps, use the Vignette effect for VR, 
and avoid the effects Lens Distortion, Chromatic Aberration, and Motion Blur for VR.

PostProcessingVolume and Volume is different. Volume is for URP. PPV is for other pipelines?
 */


public delegate void EventHandler_NewBrightness(float newValue);
/// <summary>
/// The goal of the Visual Manager is to keep track of commonly used visual related settings
/// 
/// Current Use:
/// Brightness for Virtual Equipment - Virtual Goggle
/// 
/// </summary>
public class Controller_Visual : MonoBehaviour
{
    public static event EventHandler_NewBrightness EVENT_NewBrightness;
    public Volume volume;
    VolumeProfile vp;
    LiftGammaGain lgg;
    ColorAdjustments colorAdjustments;
    float m_LightIntensity = 0;

    float minValue = -3;
    float maxValue = 3;
    float offsetValue = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        if(volume)
        {
            vp = volume.profile;
            LiftGammaGain tmp;
            if (volume.profile.TryGet<LiftGammaGain>(out tmp))
            {
                lgg = tmp;
            }
            ColorAdjustments tmp2;
            if (volume.profile.TryGet<ColorAdjustments>(out tmp2))
            {
                colorAdjustments = tmp2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        DebugUpdate();
    }

    private void DebugUpdate()
    {
        ////For Debugging
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            AdjustBrightness(-0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            AdjustBrightness(0.1f);
        }
    }

    public void AdjustBrightness(float f)
    {
        m_LightIntensity += f;
        //Dev.Log("New Light: " + f);
        SetBrightness(m_LightIntensity);
    }

    public float GetBrightness()
    {
        return m_LightIntensity;
    }

    public void SetBrightness(float f)
    {
        if (f > maxValue)
        {
            f = maxValue;
        }
        else if (f < minValue)
        {
            f = minValue;
        }

        EVENT_NewBrightness?.Invoke(f);

        m_LightIntensity = f;
        //if(lgg)
        //{
        //    lgg.gamma.SetValue(new Vector4Parameter(new Vector4(1, 1, 1, f - 1), true));
        //}
        if(colorAdjustments)
        {
            VolumeParameter<float> vpf = new VolumeParameter<float>();
            vpf.value = f-offsetValue;
            colorAdjustments.postExposure.SetValue(vpf);
        }
    }
}