using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePrerequisiteManager : MonoBehaviour
{
    public DialoguePrerequisite[] dialoguePrerequisites;
    [System.Serializable]
    public class DialoguePrerequisite
    {
        public string triggerWord;
        public bool satisfied;
    }

    public void PrerequisiteSatisfied(string triggerWord)
    {
        for(int i = 0; i < dialoguePrerequisites.Length; i++)
        {
            if(dialoguePrerequisites[i].triggerWord == triggerWord)
            {
                dialoguePrerequisites[i].satisfied = true;
            }
        }
    }
}
