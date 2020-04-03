using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Slider_Audio : MonoBehaviour
{
    public AudioMixer mixer;
    Text Text_volumeValue;
    public Audio_Type type;
    Controller_Audio audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        audioManager = Core.Ins.AudioManager;
    }

    public void SetVolume(float f)
    {
        audioManager.SetVolume(f, type);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
