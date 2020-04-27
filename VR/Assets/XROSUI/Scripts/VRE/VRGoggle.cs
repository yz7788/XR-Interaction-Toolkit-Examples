using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof(XRGrabInteractable))]
public class VRGoggle : VREquipment
{
    public float lightIncreaseRate = 0.01f;
    public float lightDecreaseRate = -0.01f;
    public Image showAndHidePanel;
    string m_pathToSave;

    public override void OnActivated(XRBaseInteractor obj)
    {
        TakeScreenshot();
    }

    void TakeScreenshot()
    {
        
        Core.Ins.AudioManager.PlaySfx("360329__inspectorj__camera-shutter-fast-a");
        Debug.Log("the Screenshot is saved in " + Application.persistentDataPath);
        string timeStamp = System.DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss");
        string fileName = "ScreenshotX" + timeStamp + ".png";//the screenshot image is name in this format, you can change it according to your need
        m_pathToSave = fileName;

        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + m_pathToSave);

        StartCoroutine(ShowAndHide(showAndHidePanel, 1.0f));
    }
    
    IEnumerator ShowAndHide(Image go, float delay)
    {
        GetPictureAndShowIt();
        go.enabled = true;
        yield return new WaitForSeconds(delay);
        go.enabled = false;
    }
    void GetPictureAndShowIt()
    {
        //files = Directory.GetFiles(Application.persistentDataPath + "/" + m_pathToSave); //to get the local files(screenshots)
        Texture2D texture = GetScreenshotImage(Application.persistentDataPath + "/" + m_pathToSave);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), 
            new Vector2(0.5f, 0.5f));//?
        showAndHidePanel.sprite = sp;
        //}
    }

    Texture2D GetScreenshotImage(string filePath)
    {
        Texture2D texture = null;
        byte[] fileBytes;
        if (File.Exists(filePath))
        {
            fileBytes = File.ReadAllBytes(filePath);
            texture = new Texture2D(2, 2, TextureFormat.RGB24, false);
            texture.LoadImage(fileBytes);
        }
        return texture;
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
