using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum TextDisplayType { Hint, Audio, System };

public class XROS_Event
{
    public TextDisplayType TargetText;
    public string content;
    public bool HasPrerequisite = false;
    public string prerequisiteFlagId;
    public string FlagIDToEnable;

    public XROS_Event(TextDisplayType t, string s)
    {
        TargetText = t;
        content = s;
        HasPrerequisite = false;
    }
    public XROS_Event(TextDisplayType t, string s, bool b, string i)
    {
        TargetText = t;
        content = s;
        HasPrerequisite = b;
        prerequisiteFlagId = i;
    }

}

public class Controller_Scenario : MonoBehaviour
{
    TextMeshProUGUI Text_Hint;
    TextMeshProUGUI Text_Audio;
    TextMeshProUGUI Text_System;

    List<XROS_Event> events = new List<XROS_Event>();
    Dictionary<string, bool> flagDictionary = new Dictionary<string, bool>();

    int currentEventId = 0;
    // Start is called before the first frame update
    void Start()
    {
        events.Add(new XROS_Event(TextDisplayType.Hint, "Move your controller to the front of your eyes until you feel a vibration.Then push the grip button to enable Augmented Vision"));
        events.Add(new XROS_Event(TextDisplayType.Hint, "Grab your user credentials located in your chest and place it on the lock"));

        events.Add(new XROS_Event(TextDisplayType.Hint, "You have successfully authenticated!", true, "AuthenticateWithHeart"));
    }

    public void SetFlag(string s, bool b)
    {
        flagDictionary[s] = b;
    }
    public bool GetFlag(string s)
    {
        bool b = false;
        try { b = flagDictionary[s]; }
        catch (KeyNotFoundException)
        {
            print("Key: " + s + "is not found");
        }
        return b;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            ProcessEvent();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            this.SetFlag("AuthenticateWithHeart", true);
        }
    }
    private void ProcessEvent()
    {
        if (currentEventId < events.Count)
        {

            XROS_Event currentEvent = events[currentEventId];
            if (!currentEvent.HasPrerequisite || (currentEvent.HasPrerequisite && GetFlag(currentEvent.prerequisiteFlagId)))
            {
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
                currentEventId++;
            }
        }
        else
        {
            print("Cannot handle event " + currentEventId);
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
