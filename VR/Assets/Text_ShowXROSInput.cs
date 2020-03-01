using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Text_ShowXROSInput : MonoBehaviour
{
    string compiledMessages = "";
    public TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        if (!text)
        {
            text = this.GetComponent<TMP_Text>();
        }
        XROSInput.EVENT_NewInput += CompileMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            text.text = "";
        }
    }
    public void CompileMessage(string s)
    {
        compiledMessages += s;
        text.text = compiledMessages;
    }

}
