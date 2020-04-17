using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class ScreenshotPreview : MonoBehaviour
{
    //public GameObject canvas;
    public Image myImage;
    string[] files = null;
    int currentImageId = 0;
    public float coolDown = 0.5f;
    private float lastAskTime = 0;

    bool bNewPictureToProcess = false;
    // Use this for initialization
    void Start()
    {
        //Application.OpenURL(Application.persistentDataPath);

        GetPictureAndShowIt();
    }
    private void OnEnable()
    {

    }

    void Update()
    {
        if (lastAskTime + coolDown < Time.time)
        //if(bNewPictureToProcess)
        {
            GetPictureAndShowIt();
            lastAskTime = Time.time;
            //bNewPictureToProcess = false;
        }
    }

    void GetPictureAndShowIt()
    {
        files = Directory.GetFiles(Application.persistentDataPath + "/", "*.png"); //to get the local files(screenshots)
        if (files.Length > 0)
        {
            string pathToFile = files[currentImageId];
            Texture2D texture = GetScreenshotImage(pathToFile);
            Sprite sp = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));
            myImage.sprite = sp;
            bNewPictureToProcess = true;
        }
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

    public void NextPicture() //to get the next screenshot
    {
        if (files.Length > 0)
        {
            currentImageId += 1;
            if (currentImageId > files.Length - 1)
            {
                currentImageId = 0;
            }
            GetPictureAndShowIt();
        }
    }

    public void PreviousPicture() //to get the previous screenshot
    {
        if (files.Length > 0)
        {
            currentImageId -= 1;
            //Debug.Log("this is the no. " + whichScreenShotIsShown + "Screenshot");
            if (currentImageId < 0)
            {
                currentImageId = files.Length - 1;
            }
            GetPictureAndShowIt();
        }
    }
}
