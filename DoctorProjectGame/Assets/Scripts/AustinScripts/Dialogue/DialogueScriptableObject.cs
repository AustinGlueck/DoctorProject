using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue / New Dialogue")]
//public class DialogueData : DialogueScriptableObject { }
public class DialogueScriptableObject : ScriptableObject
{
    public int npcID;
    public string npcName;
    public NPCDialogue[] NPCDialogues;
}

[System.Serializable]
public class NPCDialogue
{
    [TextArea(10,40)]
    public string text;
    public Response[] responses;
}

[System.Serializable]
public class Response
{
    public int nextDialogue;
    public int buttonNumber;
    [TextArea(3,10)]
    public string reply;
    public string prereq;
    public string trigger;

}

