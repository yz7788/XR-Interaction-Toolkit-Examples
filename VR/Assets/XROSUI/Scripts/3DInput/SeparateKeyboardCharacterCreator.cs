using UnityEngine;
using UnityEngine.UI; //create public inputfield 
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using System.IO;
using System;

public class SeparateKeyboardCharacterCreator: KeyboardController
{
    [System.Serializable]
    private class KeyboardWrapper
    {
        public string keyboardName;
        public List<KeyWrapper> keys = new List<KeyWrapper>();
    }
    [System.Serializable]
    private class KeyWrapper
    {
        public String text;
        public float x;
        public float y;
        public float z;
    }

    XRGrabInteractable m_InteractableBase;
    public GameObject system;
    public int segments = 10;
    public float xradius = 0.01f;
    public float yradius = 0.01f;
    public float smallerXradius;
    public float smallerYradius;
    //Prefab for a 3D key
    public GameObject PF_Key;
    public Button Button_Timer;

    public bool active = false;
    List<GameObject> points = new List<GameObject>();
    private KeyboardWrapper kw = new KeyboardWrapper();

    // Update is called once per frame
    void Update()
    {
    }
    
    public void CreateMirrorKeyboard(float startingX, float startingY, float startingZ)
    {

        // creating the actual keyboard at the bottom
        CreatePoints(startingX, startingY, startingZ);

        // creating the mirror keyboard on top
        MirrorKeys(startingX, startingY + 0.4f, startingZ);
        print(points.Count + " " + kw.keys.Count);
/*        if (kw != null && kw.keys.Count != 0)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].transform.position = new Vector3(kw.keys[i].x, kw.keys[i].y, kw.keys[i].z);
            }
        }*/
    }

    
    public void CreatePoints(float startingX, float startingY, float startingZ)
    {
        // delete
        GameObject go = CreateKey(-0.15f + startingX, 0.14f + startingY, 0.05f + startingZ, "DEL");
        Vector3 scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
        go = CreateKey(0.15f + startingX, 0.14f + startingY, 0.05f + startingZ, "DEL"); 
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
        CreateLine(-0.15f + startingX, 0.06f +startingY, -0.05f+startingZ, -10f, smallerXradius, smallerYradius, "qwert");
        CreateLine(0.15f + startingX, 0.06f + startingY, -0.05f + startingZ, 10f, smallerXradius, smallerYradius,"yuiop");
        CreateLine(-0.15f + startingX, 0f+ startingY, startingZ, -10f, xradius, yradius,"asdfg");
        CreateLine(0.15f + startingX, 0f + startingY, startingZ, +10f, xradius, yradius,"hjkl;");
        CreateLine(-0.15f + startingX, -0.06f + startingY, -0.05f + startingZ, -10f, smallerXradius, smallerYradius,"zxcv");
        CreateLine(0.15f + startingX, -0.06f + startingY, -0.05f + startingZ, 10f, smallerXradius, smallerYradius, "bnm,");
        //GameObject del = CreateKey(0.15f, startingY, 0.1f + startingZ, "DEL");

        // space
        go = CreateKey(-0.15f + startingX, -0.18f+startingY, 0.05f+startingZ, "start");
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
        go = CreateKey(0.15f + startingX, -0.18f+startingY, 0.05f + startingZ, " ");
        scale = go.transform.localScale;
        scale.x = 2 * scale.x;
        go.transform.localScale = scale;
    }

    void CreateLine(float offsetX, float offsetY, float offsetZ, float angleOffset, float xradius, float yradius, string letters)
    {
        float x;
        float z;

        float angle = 300f - letters.Length * 2f + angleOffset;
        for (int i = 0; i < (letters.Length); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;
            CreateKey(x + offsetX, offsetY, z + offsetZ, "" + letters[i]);
            angle += (180f / (letters.Length)) % 360;
        }
    }

    GameObject CreateKey(float x, float y, float z, string s)
    {
        GameObject go = Instantiate(PF_Key, new Vector3(x, y, z), new Quaternion(0, 0, 0, 0)); ;
        go.transform.SetParent(this.transform);
        XRKey key = go.GetComponent<XRKey>();
        key.Setup(s, this, Button_Timer);
        points.Add(go);
        KeyWrapper keywrappper = new KeyWrapper();
        keywrappper.text = s;
        keywrappper.x = x;
        keywrappper.y = y;
        keywrappper.z = z;
        kw.keys.Add(keywrappper);
        return go;
    }

    // remove the whole keyboard
    public void DestroyPoints()
    {
        foreach (GameObject point in points){
            Destroy(point);
        }
        points.Clear();
        kw.keys.Clear();
    }


    void MirrorKeys(float startingX, float startingY, float startingZ)
    {
        CreatePoints(startingX, startingY, startingZ);
    }

    public void SaveKeyPositions()
    {   

        string filename = "positions.JSON";
        FileStream stream = new FileStream(filename, FileMode.OpenOrCreate);
        string json;
        kw.keyboardName = "lower2";
        json = JsonUtility.ToJson(kw);
        StreamWriter writer = new StreamWriter(stream, System.Text.Encoding.UTF8);

        try
        {
            writer.Write(json);
        }
        catch (Exception exp)
        {
            print(exp.Message);
        }
        finally
        {
            writer.Close();
        }
        print(json);
    }
    public int ReadKeyPositions()
    {
        String json;
        try
        {
            json = File.ReadAllText("positions.JSON");
            kw = JsonUtility.FromJson<KeyboardWrapper>(json);
        }
        catch (Exception exp)
        {
            print(exp.Message);
        }

        if (kw != null && kw.keys!=null && kw.keys.Count!=0)
        {
            return 0;
        }

        print("null or empty");
        return -1;
    }
}

