using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Slider_Audio : MonoBehaviour
{
    public AudioMixer mixer;
    //public Text text;
    //float musicVol;
    //Slider volumSlider;
    Text Text_volumeValue;
    public Audio_Type type;
    new Controller_Audio audio;
    // Start is called before the first frame update
    void Start()
    {
        /*
        GameObject text = GameObject.Find("Text_volumeValue");
       
        mixer.GetFloat("MusicVol", out musicVol);
        if (text != null)
        {
            Text_volumeValue = text.GetComponent<Text>();
            if (Text_volumeValue != null)
            {
                Text_volumeValue.text = musicVol.ToString();
            }
            else { Debug.LogError("[" + text.name + "]- Dose not contain a Text component"); }
        }
        else { Debug.LogError("Could not find Text_volumeValue"); };
        */
          /*GameObject slider = GameObject.Find("Slider_Music");
        if (slider != null)
        {
            volumSlider = slider.GetComponent<Slider>();
            if(volumSlider != null)
            {
                volumSlider.normalizedValue = musicVol;
            }
            else { Debug.LogError("[" + slider.name + "]- Dose not contain a slider component"); }
        }
        else{ Debug.LogError("Could not find Slider_Music"); };
        */
        audio = Core.Ins.AudioManager;
        
    }

    public void SetVolume(float f)
    {
         audio.SetVolume(f, type);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
        mixer.GetFloat("MusicVol", out musicVol);
        //Debug.Log(musicVol);
        //Mathf.Pow(10f, musicVol / 20f);
        Text_volumeValue.text = "Volume:" + ((int)(Mathf.Pow(10f, musicVol / 20f)*100f)).ToString() + "%";
        //volumSlider.normalizedValue = musicVol;
        //Debug.Log(Text_volumeValue.text);
        */
    }
}
