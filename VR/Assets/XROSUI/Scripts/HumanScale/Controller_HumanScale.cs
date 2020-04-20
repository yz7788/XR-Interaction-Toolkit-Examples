using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_HumanScale : MonoBehaviour
{
    float leftArmLength = 0.635f;
    float eyeHeight = 1.6f;
    IDictionary<int, float> boneLengthDict = new Dictionary<int, float>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < System.Enum.GetValues(typeof(BoneIdx)).Length; i++)
        {
            boneLengthDict.Add(i, 0.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setBoneLength(int boneIdx, float newValue)
    {
        boneLengthDict[boneIdx] = newValue;
    }

    public float GetEyeHeight()
    {
        return eyeHeight;
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

enum BoneIdx
{
    Neck,
    Spine,
    RightHip,
    LeftHip,
    LeftUpperLeg,
    RightUpperLeg,
    LeftLowerLeg,
    RightLowerLeg,
    LeftFoot,
    RightFoot,
    LeftShoulder,
    RightShoulder,
    LeftLowerArm,
    LeftUpperArm,
    RightLowerArm,
    RightUpperArm
}
