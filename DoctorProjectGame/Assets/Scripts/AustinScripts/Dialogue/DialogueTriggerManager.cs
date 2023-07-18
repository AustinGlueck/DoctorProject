using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerManager : MonoBehaviour
{
    public DialoguePlotEvents[] dialoguePlotEvents;
    public DialogueMaterialEvents[] dialogueMaterialEvents;
    public DialogueSwitchEvents[] dialogueSwitchEvents;
    public DialogueEndEvents[] dialogueEndEvents;
    //public List<CureEvents> cureEvents; //As new patients are added, we should add new cure events that contain the patient name, when they're cured we can find their name and run the cure script
    public PotionPocket potionPocket;

    [System.Serializable]
    public class DialoguePlotEvents
    {
        public string triggerWord;
    }
    [System.Serializable]
    public class DialogueMaterialEvents
    {
        public string triggerWord;
        public Renderer objectRenderer;
        public Material material;
    }
    [System.Serializable]
    public class DialogueSwitchEvents
    {
        public string triggerWord;
        //public Interactable dialogueToChange;
        public DialogueScriptableObject newDialogue;
    }
    /*[System.Serializable]
    public class CureEvents
    {
        public string triggerWord;
        public PatientScript patientScript;
    }*/

    [System.Serializable]
    public class DialogueEndEvents
    {
        public string triggerWord;
    }




    public void TriggerSet(string trigger)
    {
        for(int i = 0; i < dialogueMaterialEvents.Length; i++)
        {
            if(dialogueMaterialEvents[i].triggerWord == trigger)
            {
                dialogueMaterialEvents[i].objectRenderer.material = dialogueMaterialEvents[i].material;
            }
        }

        for(int i = 0; i < dialogueSwitchEvents.Length; i++)
        {
            if(dialogueSwitchEvents[i].triggerWord == trigger)
            {
                //dialogueSwitchEvents[i].dialogueToChange.dialogueObject = dialogueSwitchEvents[i].newDialogue;
            }
        }

        /*for(int i = 0; i < cureEvents.Count; i++)
        {
            if(cureEvents[i].triggerWord == trigger)
            {
                potionPocket.targetPatient = cureEvents[i].patientScript;
                potionPocket.ApplyCure();
            }
        }*/

    }
}
