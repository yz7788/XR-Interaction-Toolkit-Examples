using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.UI; //create public inputfield 

public class KeyboardController : MonoBehaviour
{
    public InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterInput(string s)
    {
        int length = inputField.text.Length;
        if (length >=18)
        {
            inputField.text = inputField.text.Substring(1, length-1);
        }
        inputField.text += s;
    }
}
