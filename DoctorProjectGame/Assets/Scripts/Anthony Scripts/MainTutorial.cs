using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTutorial : MonoBehaviour
{
    public static MainTutorial Instance { get; private set; }
    public bool enableTutorial = false;
    private int numberOfStages = 10;
    private List<bool> stages = new List<bool>();
    public int currentStage = 0;
    [SerializeField] private TutorialObject interactBedObj;

    [Serializable]
    public class TutorialObject
    {
        public List<GameObject> tObj = new List<GameObject>();

        public void Enable()
        {
            foreach (GameObject obj in tObj) obj.SetActive(true);
        }

        public void Disable()
        {
            foreach (GameObject obj in tObj) obj.SetActive(false);
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        for (int i=0; i<numberOfStages; i++) stages.Add(false);

        if (!enableTutorial)
        {
            interactBedObj.Disable();
        }
    }

    private void Update()
    {
        PlayTutorials();
    }

    private void PlayTutorials()
    {
        if (enableTutorial)
        {
            InteractWithBed();
        }
    }

    public void PassCurrentStage()
    {
        stages[currentStage] = true;
    }

    private void NextStage()
    {
        currentStage += 1;
    }

    //stages
    private void InteractWithBed()
    {
        if (currentStage == 0)
        {
            interactBedObj.Enable();

            if (stages[currentStage] == true)
            {
                interactBedObj.Disable();
                NextStage();
            }
        }
    }

    private void DiagnosisPatient()
    {

    }

    private void TakeNotesOnChart()
    {

    }

    private void OpenJournal()
    {

    }

    private void GoToAlchmeyRoom()
    {

    }

    private void MoveIngredients()
    {

    }

    private void ProcessIngredients()
    {

    }

    private void MakeCure()
    {

    }

    private void BringCurePatient()
    {

    }

    private void ApplyCure()
    {

    }

    private void EndTutorial()
    {
        enableTutorial = false;
    }
}
