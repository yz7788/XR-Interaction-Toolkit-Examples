using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerClass : MonoBehaviour
{
    public static string targetText = "Barbara had been waiting at the table for twenty minutes. it had been twenty long and excruciating minutes. David had promised that he would be on time today. He never was, but he had promised this one time. She had made him repeat the promise multiple times over the last week until she'd believed his promise. Now she was paying the price.";
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
        }
        else
        {
            //testTarget.SetActive(false);
            btimerStarted = false;
            CalculateSpeed(currentTime);
            startTime = 0;
            currentTime = startTime;
        }
    }

    void CalculateSpeed(float time)
    {
        float wordsPerMinute = 0;
        int numWords = myInputContent.text.Split(' ').Length;
        Dev.Log(numWords);
        wordsPerMinute = numWords / (time/60);
        content.text = "Your input speed is "+wordsPerMinute;
    }
}
