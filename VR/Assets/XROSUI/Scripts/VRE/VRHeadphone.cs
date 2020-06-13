using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRHeadphone : VREquipment
{
    public VRHeadphone VH;
    public GameObject GestureCore;
    private float coolDown = 0.2f;
    private float lastAskTime = 0;
    //public float volumeIncreaseRate = 0.003f;
    //public float volumeDecreaseRate = -0.003f;

    public void Start()
    {

    }

    public override void OnActivated(XRBaseInteractor obj)
    {
        Core.Ins.AudioManager.PlayPauseMusic();
    }

    //public override void OnDeactivated(XRBaseInteractor obj)
    //{
    //}

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Core.Ins.AudioManager.AdjustVolume(1, Audio_Type.master);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Core.Ins.AudioManager.AdjustVolume(-1, Audio_Type.master);
        }

        if (!VH.m_Held) {
            ResetPosition();
        }
    }
    public override void HandleGesture(ENUM_XROS_Gesture gesture)
    {
        Debug.Log("HandleGesture");
        Debug.Log("lastAskTime" + lastAskTime + "currentTime" + Time.time);
        if (lastAskTime + coolDown < Time.time)
        {
            lastAskTime = Time.time;
            Debug.Log("Finish Cool down");
            switch (gesture)
            { 
                case ENUM_XROS_Gesture.up:
                    Core.Ins.AudioManager.AdjustVolume(1, Audio_Type.master);
                    Debug.Log("Gesture up");
                    break;
                case ENUM_XROS_Gesture.down:
                    Core.Ins.AudioManager.AdjustVolume(-1, Audio_Type.master);
                    //Debug.Log("downdecrease");
                    break;
                case ENUM_XROS_Gesture.forward:
                    break;
                case ENUM_XROS_Gesture.backward:
                    break;
                case ENUM_XROS_Gesture.rotate_clockwise:
                    break;
                case ENUM_XROS_Gesture.rotate_counterclockwise:
                    break;
                default:
                    break;
            }
        }
    }

    public void ResetPosition()
    {
        VH.transform.position = GestureCore.transform.position;
    }
}
