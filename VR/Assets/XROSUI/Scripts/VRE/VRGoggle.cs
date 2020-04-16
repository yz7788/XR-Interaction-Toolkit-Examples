using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRGoggle : VREquipment
{
    new XROSMenuTypes menuTypes = XROSMenuTypes.Menu_Visual;
    public XRGrabInteractable XRGrabGoggle;
    //ButtonHandler Screenshot;

    public float lightIncreaseRate = 0.01f;
    public float lightDecreaseRate = -0.01f;

    Controller_Audio audioManager;
    public void Start()
    {
        //fakeSocket = this.transform.parent.gameObject;
    }

    public void Awake()
    {
        audioManager = Core.Ins.AudioManager;
        //Screenshot = ButtonHandler.Ins;
    }

    public override void TriggerFunction()
    {
        //Screenshot.TakeAShot();
        audioManager.PlaySfx("360329__inspectorj__camera-shutter-fast-a");
        //    StartCoroutine("CaptureIt");
        Debug.Log("the Screenshot is saved in " + Application.persistentDataPath);
        string timeStamp = System.DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss");
        string fileName = "ScreenshotX" + timeStamp + ".png";//the screenshot image is name in this format, you can change it according to your need
        string pathToSave = fileName;

        //ScreenCapture.CaptureScreenshot(pathToSave);
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + pathToSave);
        
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
