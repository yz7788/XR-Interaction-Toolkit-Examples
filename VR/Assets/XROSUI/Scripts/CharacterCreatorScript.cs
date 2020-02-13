using System.Collections;
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
        GameObject go = Instantiate(PF_Key, new Vector3(0, 0, 0), Quaternion.identity);
        print(go.name);
        Key key = go.GetComponent<Key>(); //这个key从哪里取？
        print(this+ " is not null");
        key.cc = this;
        
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
        //cube a: how to call this function onHoverEnter?
    }
}
