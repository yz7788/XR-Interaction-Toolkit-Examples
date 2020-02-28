using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevLog : MonoBehaviour
{
    string logMessage = "";
    // Start is called before the first frame update
    void Start()
    {
        //Dev.EventHandler_NewLog += CompileMessage;
        
        Dev.Log("hello");   

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CompileMessage(string s)
    {
        logMessage += s;
    }
}
