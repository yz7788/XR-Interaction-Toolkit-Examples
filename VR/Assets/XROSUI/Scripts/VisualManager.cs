using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualManager : MonoBehaviour
{
    public Light lightIntensity;

    public void AdjustLightIntensity(float f)
    {
        print(f);
        lightIntensity.intensity = f;
    }

}
