using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Tool_TestVolume : MonoBehaviour
{
    //public AudioMixer Mixer;
    //public AudioClip clip;
    public string AudioClipName = "";
    public Audio_Type m_audioType;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TestVolume()
    {
        switch (m_audioType)
        {
            case Audio_Type.master:
                Core.Ins.AudioManager.PlayMusic(AudioClipName);
                break;
            case Audio_Type.music:
                Core.Ins.AudioManager.PlayMusic(AudioClipName);
                break;
            case Audio_Type.sfx:
                Core.Ins.AudioManager.Play2DAudio(AudioClipName);
                break;
            case Audio_Type.voice:
                Core.Ins.AudioManager.PlayMusic(AudioClipName);
                break;
        }
    }
}
