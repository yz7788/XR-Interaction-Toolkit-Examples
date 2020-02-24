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
        inputField.text += s;
    }
}
