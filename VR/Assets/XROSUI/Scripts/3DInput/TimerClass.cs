using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using TMPro;

public class TimerClass : MonoBehaviour
{
    public static string targetText = "barbara had been waiting at the table for twenty minutes";
    float startTime;
    float currentTime;
    public Text Text_Timer;
    public Button Button_Timer;
    public Text Text_Button_Timer;
    //public TMP_Text testTarget;
    public GameObject testTarget;
    public TMP_Text content;
    public TMP_Text myInputContent;
    bool btimerStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        //Text_Timer = this.GetComponent<Text>();
        myInputContent.text = "";
        Text_Button_Timer.text = "Start";
        testTarget.SetActive(false);
        //SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (btimerStarted)
        {
            currentTime += Time.deltaTime;
            Text_Timer.text = currentTime.ToString("0.0");
        }
        else
        {
            Text_Button_Timer.text = "start"; 
        }
        
        if (string.Compare(myInputContent.text,targetText.Substring(0, myInputContent.text.Length)) == 0)
        {
            print("the same");
            content.color = Color.cyan;
        }
        else
        {
            print("length "+myInputContent.text.Length);
            print("my "+myInputContent.text);
            print("target "+targetText.Substring(0, myInputContent.text.Length));
        }
    }
    public void SetTimer()
    {
        if (!btimerStarted)
        {
            testTarget.SetActive(true);
            btimerStarted = true;
            startTime = 0;
            currentTime = startTime;
            content.text = targetText;
            myInputContent.text = ""; // clear up input to start trial
            XROSInput.RemoveInput();
        }
        else
        {
            //testTarget.SetActive(false);
            btimerStarted = false;
            CalculateSpeed(currentTime);
            startTime = 0;
            currentTime = startTime;
            myInputContent.text = ""; // clear up input for next trial
            XROSInput.RemoveInput();
        }
    }

    void CalculateSpeed(float time)
    {
        float wordsPerMinute = 0;
        int numWords = myInputContent.text.Trim().Split(' ').Length;
        wordsPerMinute = numWords / (time/60);
        content.text = "Your input speed is "+wordsPerMinute+" words per minute";
        
    }
}
