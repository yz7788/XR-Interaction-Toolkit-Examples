﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //create public inputfield 
public class CharacterCreatorScript : MonoBehaviour
{
    public InputField inputField;
    //public Transform key;
    public GameObject PF_Key; //Prefab for a 3D key
    private string inputString;

    private void Awake()
    {
        //do some loops to generate QWERTY
        for(int i=0; i<26; i++)
        {
            GameObject go = Instantiate(PF_Key, new Vector3(1, i*0.21f, 1), Quaternion.identity);
            go.transform.SetParent(this.transform);
            XRKey key = go.GetComponent<XRKey>(); //这个key从哪里取？
            string s = ""+(char)(i + 97);
            key.myKey = s;
            key.gameObject.name = "Key: "+s;
            print(key.myKey + " " + i);
            key.characterCreator = this;

            Text t=go.GetComponent<Text>();
            t.text = s;
        }
        
        
        //what if I want to arrange these 26 blocks in some order?
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            inputField.text += "z";
            //key.DisplayInput(inputField, 'z');
        }
    }

    //TODO Handle information from Key here
    public void RegisterInput(string s)
    {
        inputField.text += s;
    }
}