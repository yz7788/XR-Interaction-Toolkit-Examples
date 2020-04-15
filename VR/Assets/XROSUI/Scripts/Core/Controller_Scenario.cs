using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using System.IO;
using TMPro;



public enum TextDisplayType { Hint, Audio, System };
public enum FlagsInControllerScenario { AuthenticateWithHeart, Flag1, Flag2 }
[Serializable]public class XROS_Event
{
    public TextDisplayType TargetText;
    public string content;
    public bool HasPrerequisite;
    public string prerequisiteFlagId;
    public int secondsToWait;//This event need to be showed for this time to go to the next event.

    private XROS_Event( TextDisplayType type, string text, bool hasPrerequisite, string flagID, int waitTime)//not use anymore
    {
        
        this.TargetText = type;
        this.content = text;
        this.HasPrerequisite = hasPrerequisite;
        this.prerequisiteFlagId = flagID;
        this.secondsToWait = waitTime;
    }
    public static XROS_Event CreateEvent(Controller_Scenario controller, TextDisplayType type, string text, bool hasPrerequisite, string flagID)//not use anymore
    {
        if (controller.addFlag(flagID))
        {
            return new XROS_Event(type,text,hasPrerequisite,flagID, -1);
        } else {
            throw new System.Exception("Flag ID has already been taken");
        }
    }
    public static XROS_Event CreateEvent(Controller_Scenario controller, TextDisplayType type, string text, int waitTime)//not use anymore
    {
        return new XROS_Event(type,text,false,null,waitTime);
    }
}
public class Controller_Scenario : MonoBehaviour
{
    TextMeshProUGUI Text_Hint;
    TextMeshProUGUI Text_Audio;
    TextMeshProUGUI Text_System;

    public bool addFlag(string flagID){//add this function so when a new XROS_Event is created, a flagID will be automatically created too.
        if(flagDictionary.ContainsKey(flagID))
        {
            return false;
        } else {
            flagDictionary.Add(flagID,false);
            return true;
        }
    }
    
    XROS_Event[] events;
    Dictionary<string, bool> flagDictionary = new Dictionary<string, bool>();
    float m_Waiting;

    int currentEventId = 0;
    // Start is called before the first frame update
    void Start()
    {
        m_Waiting=-1f;//default value -1
        // events = new XROS_Event[3];
        // events[0] = XROS_Event.CreateEvent(this, TextDisplayType.Hint, "Move your controller to the front of your eyes until you feel a vibration.Then push the grip button to enable Augmented Vision",true,"AV");
        // events[1] = XROS_Event.CreateEvent(this, TextDisplayType.Hint, "Grab your user credentials located in your chest and place it on the lock",true,"OpenDoor");
        // events[2] = XROS_Event.CreateEvent(this, TextDisplayType.Hint, "You have successfully authenticated!",3);
        // //Convert to JSON
        // string eventToJson = JsonHelper.ToJson(events, true);
        // File.WriteAllText(Application.dataPath+"/XROSUI/JSON/XROS_Event.json",eventToJson);
        //code above is used to create json file, but you don't need to use it. 

        string jsonString = File.ReadAllText(Application.dataPath+"/XROSUI/JSON/XROS_Event.json");//read the file
        events = JsonHelper.FromJson<XROS_Event>(jsonString);//deserilize it
        CheckFlag();//make sure every flag in the list is unique.
    }
    private bool CheckFlag(){
        foreach(XROS_Event e in events){
            if(e.HasPrerequisite&&!this.addFlag(e.prerequisiteFlagId))
                throw new Exception("Flag ID has already been taken");//throw exception when people are adding redundent flagID
        }
        return true;
    }
    
    public void SetFlag(string flagID, bool value)
    {
        if(flagDictionary.ContainsKey(flagID)) flagDictionary[flagID] = value;
        else throw new Exception("flagID not exists");//alert people that they had typos.
    }
    public bool GetFlag(string flagID)
    {
        return flagDictionary[flagID];
    }
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Alpha9))
        // {
        //     ProcessEvent();
        // }
        // if (Input.GetKeyDown(KeyCode.Alpha0))
        // {
        //     this.SetFlag("AuthenticateWithHeart", true);
        // }
        if(m_Waiting>=0) m_Waiting-=Time.deltaTime;// this is the timer for event disappear when time's up.
        ProcessEvent();//check if the text panel need to go to the next event.
    }
    private void ProcessEvent()
    {
        XROS_Event currentEvent = events[currentEventId];
        if (!currentEvent.HasPrerequisite || (currentEvent.HasPrerequisite && GetFlag(currentEvent.prerequisiteFlagId)))
        {
            if(m_Waiting<0){// tome is up, go to the new event.
                currentEventId++;//sequence+1
                if(currentEventId<events.Length){//make sure we have not reached to the end.
                    currentEvent = events[currentEventId];
                    m_Waiting=events[currentEventId].secondsToWait;
                    switch (currentEvent.TargetText)
                    {
                        case TextDisplayType.Hint:
                            Text_Hint.text = currentEvent.content;
                            break;
                        case TextDisplayType.Audio:
                            Text_Audio.text = currentEvent.content;
                            break;
                        case TextDisplayType.System:
                            Text_System.text = currentEvent.content;
                            break;
                        default:
                            print("Cannot handle " + currentEvent.TargetText);
                            break;
                    }
                } else {//Reach to the end of events so clear the text.
                    Text_Hint.text = "";
                    Text_Audio.text = "";
                    Text_System.text = "";
                }
                
            }
        }
    }

    //This is for Text Panels to register themselves to the ScenarioManager
    public void Register(TextMeshProUGUI text, TextDisplayType type)
    {
        switch (type)
        {
            case TextDisplayType.Hint:
                Text_Hint = text;
                break;
            case TextDisplayType.Audio:
                Text_Audio = text;
                break;
            case TextDisplayType.System:
                Text_System = text;
                break;
            default:
                print("Cannot handle " + type);
                break;
        }
    }
}

public static class JsonHelper//helper for json process, do not change.
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
