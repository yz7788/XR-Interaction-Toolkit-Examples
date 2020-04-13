using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonHandler : MonoBehaviour
{

    //[SerializeField]
    //GameObject blink;
    Text myButton;
    public void Start()
    {
        myButton = GetComponentInChildren<Text>();
    }
    public void TakeAShot()
    {
        //    StartCoroutine("CaptureIt");
        myButton.text = Application.persistentDataPath;
        Debug.Log(Application.persistentDataPath);// "Application.persistentDataPath" is the file path to save the screenshots, you can change it according to your need
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
