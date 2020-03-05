using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider_Audio : MonoBehaviour
{

    public Audio_Type type;
    new Controller_Audio audio;
    // Start is called before the first frame update
    void Start()
    {
        audio = Core.Ins.AudioManager;
    }

    public void SetVolume(float f)
    {
        audio.SetVolume(f, type);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
