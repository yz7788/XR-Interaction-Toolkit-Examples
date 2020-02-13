using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //create public inputfield 
public class CharacterCreatorScript : MonoBehaviour
{
    public InputField inputField;
    public Transform key;

    private string inputString;

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


}
