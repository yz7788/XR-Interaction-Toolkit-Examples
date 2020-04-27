using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class ButtonHandler : MonoBehaviour
{
    public TextMeshProUGUI myButton;
    
    public void Start()
    {
    }

    public void TakeAShot()
    {
        Core.Ins.AudioManager.PlaySfx("360329__inspectorj__camera-shutter-fast-a");
        myButton.SetText("Screenshot Got!");
        Debug.Log("The Screenshot is saved as " + Application.persistentDataPath);// "Application.persistentDataPath" is the file path to save the screenshots, you can change it according to your need
        string timeStamp = System.DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss");
        string fileName = "ScreenshotX" + timeStamp + ".png";//the screenshot image is name in this format, you can change it according to your need
        string pathToSave = fileName;

        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + pathToSave);       
    }

    IEnumerator CaptureIt()
    {
        yield return new WaitForEndOfFrame();
        //Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);
    }
}