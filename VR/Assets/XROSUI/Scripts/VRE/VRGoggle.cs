using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRGoggle : VREquipment
{

    public XRGrabInteractable XRGrabGoggle;

    public float lightIncreaseRate = 0.01f;
    public float lightDecreaseRate = -0.01f;

    public void Start()
    {
        //fakeSocket = this.transform.parent.gameObject;
    }

    public override void HandleGesture(ENUM_XROS_Gesture gesture)
    {
        switch (gesture)
        {
            case ENUM_XROS_Gesture.up:
                break;
            case ENUM_XROS_Gesture.down:
                break;
            case ENUM_XROS_Gesture.forward:
                break;
            case ENUM_XROS_Gesture.backward:
                break;
            case ENUM_XROS_Gesture.left:
                Core.Ins.VisualManager.AdjustBrightness(lightDecreaseRate);
                //Debug.Log("decreaselight");
                break;
            case ENUM_XROS_Gesture.right:
                Core.Ins.VisualManager.AdjustBrightness(lightIncreaseRate);
                //Debug.Log("increaselight");
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
