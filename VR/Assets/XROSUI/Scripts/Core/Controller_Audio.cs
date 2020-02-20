
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


//Supported Audio Formats
//https://docs.unity3d.com/Manual/AudioFiles.html


//Design Note:
//a_source is used for basic system UI sound effects (such as error)
//musice_source is used for music.
//Every other sound effect would be requested and created through an object pooler.
public class Controller_Audio : MonoBehaviour
{
    //Public so we can drag child objects
    [Tooltip("Drag child object with an audiosource to be used as the default menu sfx audio source")]
    public AudioSource menuSfx_source;
    [Tooltip("Drag child object with an audiosource to be used as the default music source")]
    public AudioSource music_source;
    [Tooltip("Drag an audio file to be the default error sound")]
    public AudioClip errorClip;
    [Tooltip("Drag a GO_AudioSource Prefab to be used for all audio effects, through ObjectPooling")]
    public GO_AudioSource PF_AudioSource;

    //Tracks the different clips we have loaded
    Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();

    //Class Save Data
    [HideInInspector]
    public SettingSaveData Setting;

    private void Awake()
    {
        PF_AudioSource.gameObject.SetActive(false);

        LoadAudioClip("ClickFeedback", "dumdum");
    }

    public void PlaySound(AudioClip clip)
    {
        GO_AudioSource obj = Instantiate(PF_AudioSource, transform);
        obj.PlaySound(clip);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    //Reference
    //https://forum.unity.com/threads/how-to-use-scenemanager-onsceneloaded.399221/
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Dev.Log("Scene Name: " + scene.name + " build index " + scene.buildIndex, Dev.LogCategory.Audio);
        AudioClip newLevelMusicClip;
        if (scene.buildIndex == 0)
        {
            newLevelMusicClip = LoadAudioClip("Memories (Loop)");
        }
        else
        {
            newLevelMusicClip = LoadAudioClip("Stay (Loop)");
        }

        Dev.Log("Playing Music: " + newLevelMusicClip.name, Dev.LogCategory.Audio);

        if (newLevelMusicClip)
        {
            music_source.clip = newLevelMusicClip;
            music_source.loop = true;
            music_source.Play();
        }
    }

    public void AdjustVolume_Master(float f)
    {
        music_source.volume = f;
    }

    public void AdjustVolume_Music(float f)
    {
        print(f);
        music_source.volume = f;
    }

    public void AdjustVolume_SoundEffects(float f)
    {
        music_source.volume = f;
    }

    #region Play Audio
    public void PlayMusic(string acname)
    {
        this.PlayMusic(LoadAudioClip(acname));
    }

    public void PlayMusic(AudioClip ac)
    {
        //Make sure we have a valid clip
        if (ac == null) return;


        AudioSource source = this.music_source;
        source.clip = ac;
        source.Play();
    }

    private void Play3DAudio(AudioClip ac, GameObject go, float pitch = 1f)
    {
        //Make sure we have a valid clip
        if (ac == null) return;

        //Handles pitch variation
        if (pitch != 1)
        {
            pitch = UnityEngine.Random.Range(1 - pitch, 1 + pitch);
        }

        //TODO ObjectPooling
        //GameObject audioObj = PoolManager.Spawn(instance.oneShotPrefab, position, Quaternion.identity);
        //AudioSource source = audioObj.GetComponent<AudioSource>();
        AudioSource source = this.menuSfx_source;

        source.clip = ac;
        source.pitch = pitch;
        source.Play();

        //deactivate audio gameobject when the clip stops playing
        //PoolManager.Despawn(audioObj, clip.length);
    }

    public void Play3DAudio(string acname, GameObject go)
    {
        this.Play3DAudio(LoadAudioClip(acname), go);
    }

    public void Play2DAudio(string acname)
    {
        this.Play3DAudio(LoadAudioClip(acname), this.gameObject);
    }
    #endregion Play Audio

    #region Load Audio
    /// <summary>
    /// Loads an Audio Clip based on the resourcename, calls the other LoadAudioClip to handle the request.
    /// </summary>
    /// <param name="resourcename"></param>
    /// <returns></returns>
    private AudioClip LoadAudioClip(string resourcename)
    {
        return LoadAudioClip(resourcename, resourcename);
    }

    /// <summary>
    /// Loads an audio into the audioDictionary based on resourcename, if it hasn't been loaded
    /// </summary>
    /// <param name="name"></param>
    /// <param name="resourcename"></param>
    /// <returns></returns>
    private AudioClip LoadAudioClip(string name, string resourcename)
    {
        AudioClip ac;
        if (audioDictionary.ContainsKey(name))
        {
            ac = audioDictionary[name];
        }
        else
        {
            ac = Resources.Load<AudioClip>(resourcename);
            if (ac != null)
            {
                Dev.Log(name + " not in dictionary, loading " + resourcename + " in Resources folder", Dev.LogCategory.Audio);
                audioDictionary.Add(name, ac);
            }
            else
            {
                Dev.Log(name + " not in dictionary and " + resourcename + " not in Resources folder. Default to error clip", Dev.LogCategory.Audio);
                //Note: To Address this issue, make sure the audio with the name you want to use is loaded into the audioDictionary.
                audioDictionary.Add(name, errorClip);
                ac = errorClip;
            }
        }

        return ac;
    }
    #endregion Load Audio

    #region Setting Save Data
    [Serializable]
    public struct SettingSaveData
    {
        public bool soundOn;
        public bool musicOn;
    }

    public SettingSaveData DefaultSaveData()
    {
        SettingSaveData saveData = new SettingSaveData
        {
            soundOn = true,
            musicOn = true,
        };
        return saveData;
    }

    public SettingSaveData GetSaveData()
    {
        return Setting;
    }

    public void LoadSaveData(SaveData saveData)
    {
        Setting = saveData.audioSetting;
    }
    #endregion
}
