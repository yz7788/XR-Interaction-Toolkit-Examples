using UnityEngine;
using UnityEngine.UI;

public class TimerClass : MonoBehaviour
{
    float startTime;
    float currentTime;

    public Text Text_Timer;
    public Button Button_Timer;
    public Text Text_Button_Timer;
    bool btimerStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        //Text_Timer = this.GetComponent<Text>();
        Text_Button_Timer.text = "Start";
        Button_Timer.onClick.AddListener(SetTimer);
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
    }
    public void SetTimer()
    {
        btimerStarted = true;
        startTime = 0;
        currentTime = startTime;
    }
}
