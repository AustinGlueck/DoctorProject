using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueMaster : MonoBehaviour
{
    [Header("Scriptable Object")]
    public DialogueScriptableObject dialogue;
    //public string[] triggerTexts;

    [Header ("Text Boxes")]
    public TextMeshProUGUI characterNameBox;
    public TextMeshProUGUI dialogueBox;
    public GameObject[] responseBoxes;
    public Button[] responseButtons;
    public TextMeshProUGUI[] responseTexts;

    [Header("Dialogue Logic")]
    public int currentDialogueStep;
    public int numberOfResponses;
    public bool inDialogue;
    private string chosenTrigger;
    //public List<string> activeResponses = new List<string>();
    public List<string> activeResponses;
    public List<int> activeButtonNumber;
    public List <string> invalidResponse;
    public List<int> invalidButtonNumber;
    public DialogueTriggerManager dialogueTriggerManager;
    public DialoguePrerequisiteManager dialoguePrerequisiteManager;
    //public DialogueMaterialTrigger dialogueMaterialTrigger;

    public void Startup()
    {
        activeResponses.Clear();
        activeButtonNumber.Clear();
        invalidResponse.Clear();
        invalidButtonNumber.Clear();
        foreach (GameObject buttonObject in responseBoxes)
        {
            buttonObject.SetActive(false);
        }

        responseButtons = new Button[responseBoxes.Length];
        for (int i = 0; i < responseBoxes.Length; i++)
        {
            responseButtons[i] = responseBoxes[i].GetComponent<Button>();
        }
        loadDialogue(0);
        characterNameBox.text = dialogue.npcName;
    }

    // Update is called once per frame

    public void buttonPressed(int buttonNumber)
    {
        for (int i = 0; i < dialogue.NPCDialogues[currentDialogueStep].responses.Length; i++) 
        {
            if (buttonNumber == dialogue.NPCDialogues[currentDialogueStep].responses[i].buttonNumber)
            {
                if(dialogue.NPCDialogues[currentDialogueStep].responses[i].trigger != null)
                {
                    chosenTrigger = dialogue.NPCDialogues[currentDialogueStep].responses[i].trigger;
                    dialogueTriggerManager.TriggerSet(chosenTrigger);
                    foreach(DialoguePrerequisiteManager.DialoguePrerequisite prereq in dialoguePrerequisiteManager.dialoguePrerequisites) // at trigger set, looks through prerequisites and set any that have a matching triggerword
                    {
                        if(prereq.triggerWord == chosenTrigger)
                        {
                            prereq.satisfied = true;
                        }
                    }
                }
                activeResponses.Clear();
                activeButtonNumber.Clear();
                invalidResponse.Clear();
                invalidButtonNumber.Clear();
                loadDialogue(dialogue.NPCDialogues[currentDialogueStep].responses[i].nextDialogue);          
            }
        }
    }

    public void loadDialogue(int nextResponse)
    {
        //activeResponses.Add("Work");
        dialogueBox.text = dialogue.NPCDialogues[nextResponse].text;
        numberOfResponses = dialogue.NPCDialogues[nextResponse].responses.Length;
        currentDialogueStep = nextResponse;
        //Prereq Test////////////////////////////////////////////////////////////////////////
        for (int i = 0; i < numberOfResponses; i++)
        {

            if (dialogue.NPCDialogues[nextResponse].responses[i].prereq == "")
            {
                activeResponses.Add(dialogue.NPCDialogues[nextResponse].responses[i].reply);
                activeButtonNumber.Add(dialogue.NPCDialogues[nextResponse].responses[i].buttonNumber);
            }
            else
            {
                for (int e = 0; e < dialoguePrerequisiteManager.dialoguePrerequisites.Length; e++)
                {

                    if (dialogue.NPCDialogues[nextResponse].responses[i].prereq == dialoguePrerequisiteManager.dialoguePrerequisites[e].triggerWord)
                    {
                        if (dialoguePrerequisiteManager.dialoguePrerequisites[e].satisfied == true)
                        {
                            activeResponses.Add(dialogue.NPCDialogues[nextResponse].responses[i].reply);
                            activeButtonNumber.Add(dialogue.NPCDialogues[nextResponse].responses[i].buttonNumber);
                        }
                        else
                        {
                            invalidResponse.Add(dialogue.NPCDialogues[nextResponse].responses[i].reply);
                            invalidButtonNumber.Add(dialogue.NPCDialogues[nextResponse].responses[i].buttonNumber);
                        }
                    }
                }
            }
            
        }

        for (int i = 0; i < responseBoxes.Length; i++)
        {
            /*for (int o = 0; o < invalidButtonNumber.Count; o++)
            {
                if(invalidButtonNumber[o] == i)
                {
                    responseBoxes[i].SetActive(true);
                    responseTexts[i].text = invalidResponse[o];
                }
            }*/
            for(int e = 0; e < activeButtonNumber.Count; e++)
            {
                if(activeButtonNumber[e] == i)
                {
                    responseBoxes[i].SetActive(true);
                    responseTexts[i].text = activeResponses[e];
                }
                else
                {
                    responseBoxes[i].SetActive(false);
                }
            }

        }

        for (int i = 0; i < activeResponses.Count; i++)
        {
            responseBoxes[activeButtonNumber[i]].SetActive(true);
            responseButtons[activeButtonNumber[i]].interactable = true;
            responseTexts[activeButtonNumber[i]].text = activeResponses[i];
        }

        for (int i = 0; i < invalidResponse.Count; i++)
        {
            responseBoxes[invalidButtonNumber[i]].SetActive(true);
            responseButtons[invalidButtonNumber[i]].interactable = false;
            responseTexts[invalidButtonNumber[i]].text = invalidResponse[i];
        }

        //Prereq Test////////////////////////////////////////////////////////////////////////


        /*for (int i = 0; i < responseBoxes.Length; i++)
        {
             if(i >= numberOfResponses)
             {
                 responseBoxes[i].SetActive(false);
             }
             if (i < numberOfResponses)
             {
                responseBoxes[i].SetActive(true);
             }

        }*/

        /*for (int i = 0; i < numberOfResponses; i++)
        {
           responseTexts[i].text = dialogue.NPCDialogues[nextResponse].responses[i].reply;
        }*/
    }

    ///For tutorial (by Anthony)
    public void PassTutorial()
    {
        if (MainTutorial.Instance != null)
        {
            if (MainTutorial.Instance.enableTutorial)
            {
                switch (MainTutorial.Instance.currentStage)
                {
                    case 1:
                        MainTutorial.Instance.PassCurrentStage();
                        break;
                }
            }
        }
    }
}
