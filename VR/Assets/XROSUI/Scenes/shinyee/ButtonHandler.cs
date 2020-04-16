using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class ButtonHandler : MonoBehaviour
{

    //[SerializeField]
    //GameObject blink;
    //Text myButton;
    public TextMeshProUGUI myButton;
    Controller_Audio audioManager;
    //private static ButtonHandler ins = null;

    public void Start()
    {
        //myButton = GetComponentInChildren<TextMeshPro>();
        //myButton = FindObjectOfType<TextMeshProUGUI>();
    }

    /*
     * public static ButtonHandler Ins
    {
        get
        {
            return ins;
        }
    }
    */
    void Awake()
    {
        audioManager = Core.Ins.AudioManager;
    }

    public void TakeAShot()
    {
        
        audioManager.PlaySfx("360329__inspectorj__camera-shutter-fast-a");
        //    StartCoroutine("CaptureIt");
        //myButton.text = Application.persistentDataPath;
        myButton.SetText(Application.persistentDataPath);
        Debug.Log(Application.persistentDataPath);// "Application.persistentDataPath" is the file path to save the screenshots, you can change it according to your need
        string timeStamp = System.DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss");
        string fileName = "ScreenshotX" + timeStamp + ".png";//the screenshot image is name in this format, you can change it according to your need
        string pathToSave = fileName;

        //ScreenCapture.CaptureScreenshot(pathToSave);
        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + pathToSave);       

    }

    IEnumerator CaptureIt()
    {
        yield return new WaitForEndOfFrame();
        //Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);
    }

}