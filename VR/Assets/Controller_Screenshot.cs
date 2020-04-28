using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public delegate void EventHandler_NewScreenshot();

public class Controller_Screenshot : MonoBehaviour
{
    public static event EventHandler_NewScreenshot EVENT_NewScreenshot;
    public TextMeshProUGUI myButton;
    public GameObject myPanel;
    Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeAShot()
    {
        Core.Ins.AudioManager.PlaySfx("360329__inspectorj__camera-shutter-fast-a");
        //Debug.Log("The Screenshot is saved in " + Application.persistentDataPath);// "Application.persistentDataPath" is the file path to save the screenshots, you can change it according to your need
        string timeStamp = System.DateTime.Now.ToString("MM-dd-yyyy-HH-mm-ss");
        string fileName = "ScreenshotX" + timeStamp + ".png";//the screenshot image is name in this format, you can change it according to your need
        string pathToSave = fileName;

        ScreenCapture.CaptureScreenshot(Application.persistentDataPath + "/" + pathToSave);
        texture  = ScreenCapture.CaptureScreenshotAsTexture();
        if (EVENT_NewScreenshot != null)
        {
            EVENT_NewScreenshot();
        }
        StartCoroutine(ShowAndHide(myPanel, 1.0f));
    }


    IEnumerator ShowAndHide(GameObject go, float delay)
    {
        myButton.enabled = true;
        myButton.SetText("Screenshot Got!");
        go.SetActive(true);
        Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
        Image image = go.GetComponent<Image>();
        image.sprite = sp;
        yield return new WaitForSeconds(delay);
        go.SetActive(false);
        myButton.enabled = false;
    }

    IEnumerator CaptureIt()
    {
        yield return new WaitForEndOfFrame();
        //Instantiate(blink, new Vector2(0f, 0f), Quaternion.identity);
    }
}
