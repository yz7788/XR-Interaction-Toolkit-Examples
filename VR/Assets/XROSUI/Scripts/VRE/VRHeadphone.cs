using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRHeadphone : VREquipment
{
    public GameObject GestureCore;

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

    //public void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.I))
    //    {
    //        Core.Ins.AudioManager.AdjustVolume(1, Audio_Type.master);
    //    }
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        Core.Ins.AudioManager.AdjustVolume(-1, Audio_Type.master);
    //    }
    //}
    public override void HandleGesture(ENUM_XROS_Gesture gesture, float distance)
    {

        switch (gesture)
        {
            case ENUM_XROS_Gesture.up:
                Core.Ins.AudioManager.AdjustVolume(1, Audio_Type.master);
                break;
            case ENUM_XROS_Gesture.down:
                Core.Ins.AudioManager.AdjustVolume(-1, Audio_Type.master);
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
