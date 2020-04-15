using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_HumanScale : MonoBehaviour
{
    float leftArmLength = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setArmLength(float newLength)
    {
        leftArmLength = newLength;
    }

    public float getArmLength()
    {
        return leftArmLength;
    }
}
