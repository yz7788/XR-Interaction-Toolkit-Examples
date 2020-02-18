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
    public GameObject row0 = null;
    public GameObject row1 = null;
    public GameObject row2 = null;
    public GameObject row3 = null;
    public GameObject row4 = null;
    private void Awake()
    {
        GameObject keys = this.transform.GetChild(0).gameObject;
        row0 = keys.transform.GetChild(0).gameObject;
        row1 = keys.transform.GetChild(1).gameObject;
        row2 = keys.transform.GetChild(2).gameObject;
        row3 = keys.transform.GetChild(3).gameObject;
        row4 = keys.transform.GetChild(4).gameObject;
        //do some loops to generate QWERTY

        
        for (int i = 0; i< 10; i++)
        {
            string s = "" + (i);
            CreateKey(s, row0, i);
        }

        for (int i = 0; i < 10; i++)
        {
            char[] row1Keys = { 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p'};
            string s = "" + row1Keys[i];
            CreateKey(s, row1, i);
        }
        
        for (int i = 0; i< 9; i++)
        {
            char[] row2Keys = { 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l' };
            string s = "" + row2Keys[i];
            CreateKey(s, row2, i);
        }

        for (int i = 0; i < 7; i++)
        {
            char[] row3Keys = { 'z', 'x', 'c', 'v', 'b', 'n', 'm'};
            string s = "" + row3Keys[i];
           CreateKey(s, row3, i);
        }

        //space
        CreateKey(" ", row4, 0);
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

    private XRKey CreateKey(string s, GameObject parent, int position)
    {
        GameObject go = Instantiate(PF_Key, new Vector3(0, 0, 0), Quaternion.identity);
        go.transform.SetParent(parent.transform);

        // set text for each keyCube
        GameObject keyName = go.transform.GetChild(0).gameObject;
        keyName.GetComponent<Text>().text = s;

        XRKey key = go.GetComponent<XRKey>(); 
        key.myKey = s;
        key.gameObject.name = "Key: " + s;
        key.characterCreator = this;
        Text t = go.GetComponent<Text>();
        t.text = s;
        return key;
    }
}
