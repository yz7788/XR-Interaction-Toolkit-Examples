using System;

[Serializable]
public class XROS_Event
{
    public TextDisplayType TargetText;
    public string content;
    public bool HasPrerequisite;
    public string prerequisiteFlagId;
    public int secondsToWait;//This event need to be showed for this time to go to the next event.

    private XROS_Event(TextDisplayType type, string text, bool hasPrerequisite, string flagID, int waitTime)//not use anymore
    {
        this.TargetText = type;
        this.content = text;
        this.HasPrerequisite = hasPrerequisite;
        this.prerequisiteFlagId = flagID;
        this.secondsToWait = waitTime;
    }
}