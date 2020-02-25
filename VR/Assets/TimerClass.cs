using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerClass : MonoBehaviour
{    
    float startTime;
    float currentTime;

 
 public Text timerText;
bool timerStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        timerText = this.GetComponent<Text>();
        //SetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(timerStarted)
        {
        currentTime += Time.deltaTime;
        timerText.text = ""+currentTime;

        }
    }
    public void SetTimer()
    {
        timerStarted = true;
        startTime = Time.time;
        currentTime =  startTime;
    }
}
