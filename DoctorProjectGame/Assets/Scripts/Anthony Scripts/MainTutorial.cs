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
    [SerializeField] private TutorialObject interactDialogueObj;
    [SerializeField] private TutorialObject interactChartObj;

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
            interactDialogueObj.Disable();
            interactChartObj.Disable();
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
            DiagnosisPatient();
            TakeNotesOnChart();
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
    private void InteractWithBed() //0
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

    private void DiagnosisPatient() //1
    {
        if (currentStage == 1)
        {
            interactDialogueObj.Enable();

            if (stages[currentStage] == true)
            {
                interactDialogueObj.Disable();
                NextStage();
            }
        }
    }

    private void TakeNotesOnChart() //2
    {
        if (currentStage == 2)
        {
            interactChartObj.Enable();

            if (stages[currentStage] == true)
            {
                interactChartObj.Disable();
                NextStage();
            }
        }
    }

    private void OpenJournal() //3
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
