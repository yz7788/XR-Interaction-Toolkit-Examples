using UnityEngine;
using UnityEngine.UI;

public class TimerClass : MonoBehaviour
{
    float startTime;
    float currentTime;

    public Text Text_Timer;
    bool btimerStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        Text_Timer = this.GetComponent<Text>();
        //SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (btimerStarted)
        {
            currentTime += Time.deltaTime;
            Text_Timer.text = "" + currentTime;
        }
    }
    public void SetTimer()
    {
        btimerStarted = true;
        startTime = Time.time;
        currentTime = startTime;
    }
}
