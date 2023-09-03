using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] private GameObject blackBackground;

    [SerializeField] private GameObject patientScreen;
    [SerializeField] private GameObject patient; // Austin
    [SerializeField] private Image patientSprite;

    [SerializeField] private TextMeshProUGUI patientNameText;
    [SerializeField] private GameObject patientSymptomsContent;
    [SerializeField] private TMP_InputField patientInputField;

    private bool viewPauseMenu = false;
    private bool viewingPatient = false;
    private bool viewingAlchemy = false;
    private bool viewingMerchant = false;

    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private Canvas alchemyCanvas;
    [SerializeField] private Canvas merchantCanvas;
    [SerializeField] private GameObject dialogueCanvas; // Austin
    [SerializeField] private DialogueMaster dialogueMaster; // Austin

    [SerializeField] private TextMeshProUGUI cureResultText; //temp for playtest 2

    private PatientGraphicsHandler activePatientGraphics; // Austin - This is a cache for the active patient graphic, so we can destroy it when the player stops looking at that patient.
    
    public bool IsViewingPauseMenu() { return viewPauseMenu; }
    public bool IsViewingScreen() { return viewingAlchemy || viewingPatient || viewingMerchant; }
    public bool IsViewingAlchemy() { return viewingAlchemy; }
    public bool IsViewingPatient() { return viewingPatient; }
    public bool IsViewingMerchant() { return viewingMerchant; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        blackBackground.SetActive(false);
        if(pauseMenuCanvas) pauseMenuCanvas.gameObject.SetActive(false);
        patientScreen.gameObject.SetActive(false);

        alchemyCanvas.gameObject.SetActive(false);
        //merchantCanvas.gameObject.SetActive(false);
        if(dialogueCanvas) dialogueCanvas.SetActive(false);

        if (cureResultText) cureResultText.gameObject.SetActive(false);
    }

    private void Update()
    {
        EscapeKeyBindings();
    }

    private void EscapeKeyBindings()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu();
            BackOutOfCurrentScreen();
        }
    }

    private void PauseMenu()
    {
        if (pauseMenuCanvas && !IsViewingScreen()) TogglePause();
    }

    public void TogglePause()
    {
        viewPauseMenu = !viewPauseMenu;
        //print(viewPauseMenu);
        PlayerController.Instance.SetPlayerMovement(!viewPauseMenu);
        pauseMenuCanvas.gameObject.SetActive(viewPauseMenu);
    }

    public void QuitDesktopApp()
    {
        MySceneManager.Instance.QuitDesktopApp();
    }

    public void BackOutOfCurrentScreen()
    {
        if (viewingPatient && TutorialCheck())
        {
            ResetPatientScreen();
        }
        else if (viewingAlchemy)
        {
            ExitAlchemyScreen();
        }
    }

    public void ViewPatientScreen(Sprite spr, string name, List<bool> checkMarks, string info, DialogueScriptableObject dialogue, PatientGraphicsHandler graphicsHandler)
    {
        if (!viewingPatient && !CheckPlayerIsViewingChart())
        {
            viewingPatient = true;
            PlayerController.Instance.SetPlayerMovement(false);
            blackBackground.SetActive(true);

            patientNameText.text = name;
            Toggle[] toggleList = patientSymptomsContent.GetComponentsInChildren<Toggle>();
            for (int i=0; i<toggleList.Length; i++)
            {
                toggleList[i].isOn = checkMarks[i];
            }

            //old way of displaying symptoms
            /*patientSymptomText.text = "- " + checkMarks[0].symptomName;
            if (checkMarks.Count > 1)
            {
                for (int i = 1; i < checkMarks.Count; i++)
                    patientSymptomText.text += "\n" + "- " + checkMarks[i].symptomName;
            }*/
            if(info != null) patientInputField.text = info;
            patientSprite.sprite = spr;
            patientScreen.gameObject.SetActive(true);
            JournalAndChart.Instance.ToggleButtons();

            dialogueCanvas.SetActive(true); // Austin
            dialogueMaster.dialogue = dialogue;
            dialogueMaster.Startup(); // Austin

            patient.SetActive(false); // Austin
            activePatientGraphics = graphicsHandler; // Austin
            graphicsHandler.InstantiateCharacter(); //Austin


            JournalAndChart.Instance.ToggleButtons();
        }
    }

    public void ResetPatientScreen()
    {
        if (viewingPatient)
        {
            SaveToggleListToBed();
            SaveNotes();
            patientScreen.gameObject.SetActive(false);
            blackBackground.SetActive(false);
            PlayerController.Instance.SetPlayerMovement(true);
            BedManager.Instance.SetActiveBed(null);
            viewingPatient = false;

            dialogueCanvas.SetActive(false); // Austin
            activePatientGraphics.DestroyGraphics(); // Austin

            JournalAndChart.Instance.ToggleButtons();

            dialogueCanvas.SetActive(false); // Austin
        }
    }

    private void SaveToggleListToBed()
    {
        Toggle[] toggleList = patientSymptomsContent.GetComponentsInChildren<Toggle>();
        List<bool> newBoolList = new List<bool>();
        for (int tl=0; tl < toggleList.Length; tl++)
        {
            newBoolList.Add(toggleList[tl].isOn);
        }
        
        Bed currentBed = BedManager.Instance.GetActiveBed();
        currentBed.SetCheckMarks(newBoolList);
    }

    private void SaveNotes()
    {
        Bed currentBed = BedManager.Instance.GetActiveBed();
        currentBed.SetNotes(patientInputField.text);
    }

    public void EnterAlchemyScreen()
    {
        if (!viewingAlchemy)
        {
            PlayerController.Instance.SetPlayerMovement(false);
            viewingAlchemy = true;
            JournalAndChart.Instance.ToggleButtons();
            blackBackground.SetActive(true);
            alchemyCanvas.gameObject.SetActive(true);
        }
    }

    public void ExitAlchemyScreen()
    {
        if (viewingAlchemy)
        {
            alchemyCanvas.gameObject.SetActive(false);
            blackBackground.SetActive(false);
            JournalAndChart.Instance.ToggleButtons();
            viewingAlchemy = false;
            PlayerController.Instance.SetPlayerMovement(true);
        }
    }

    private bool CheckPlayerIsViewingChart() { return JournalAndChart.Instance.IsViewing(); }

    public void EnterMerchantScreen()
    {
        if (!viewingMerchant && merchantCanvas)
        {
            viewingMerchant = true;
            PlayerController.Instance.SetPlayerMovement(false);
            blackBackground.SetActive(true);

            merchantCanvas.gameObject.SetActive(true);
            MerchantManager.Instance.SetupMerchant();
        }
    }

    public void ExitMerchantScreen()
    {
        if (viewingMerchant && merchantCanvas)
        {
            merchantCanvas.gameObject.SetActive(false);

            blackBackground.SetActive(false);
            PlayerController.Instance.SetPlayerMovement(true);
            viewingMerchant = false;
        }
    }

    //Temp for playtest2
    public void DisplayCureResult(bool b)
    {
        if (cureResultText != null)
        {
            cureResultText.color = b ? Color.green : Color.yellow;
            cureResultText.text = b ? "Cured!" : "Hmm...";
            cureResultText.gameObject.SetActive(true);
            StartCoroutine(LowerTextOpacity(cureResultText));
        }
    }

    IEnumerator LowerTextOpacity(TextMeshProUGUI textObj)
    {
        while(textObj.color.a > 0)
        {
            float alphaVal = textObj.color.a - 0.1f;
            Color newColor = textObj.color;
            newColor.a = alphaVal;
            textObj.color = newColor;
            yield return new WaitForSeconds(0.5f);
        }
        //yield return null;
    }

    public void PassTutorialChart()
    {
        if (MainTutorial.Instance != null)
        {
            if (MainTutorial.Instance.enableTutorial)
            {
                switch (MainTutorial.Instance.currentStage)
                {
                    case 2:
                        MainTutorial.Instance.PassCurrentStage();
                        break;
                }
            }
        }
    }

    private bool TutorialCheck()
    {
        if (MainTutorial.Instance != null)
        {
            return MainTutorial.Instance.enableTutorial && MainTutorial.Instance.currentStage == 3;
        }
        else
        {
            return true;
        }
    }
}
