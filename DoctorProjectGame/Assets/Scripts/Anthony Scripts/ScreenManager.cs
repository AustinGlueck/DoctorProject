using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] private GameObject blackBackground;
    [SerializeField] private Image patientScreen;
    [SerializeField] private Image patientScreenChart;
    [SerializeField] private TextMeshProUGUI patientNameText;
    [SerializeField] private TextMeshProUGUI patientSymptomText;
    [SerializeField] private TextMeshProUGUI patientInfoText;

    private bool viewingPatient = false;
    private bool viewingAlchemy = false;
    private bool viewingMerchant = false;

    [SerializeField] private Canvas alchemyCanvas;
    [SerializeField] private Canvas merchantCanvas;
    [SerializeField] private GameObject dialogueCanvas; // Austin
    [SerializeField] private DialogueMaster dialogueMaster; // Austin

    [SerializeField] private TextMeshProUGUI cureResultText; //temp for playtest 2

    public bool IsViewingScreen() { return viewingAlchemy || viewingPatient || viewingMerchant; }
    public bool IsViewingAlchemy() { return viewingAlchemy; }
    public bool IsViewingPatient() { return viewingPatient; }
    public bool IsViewingMerchant() { return viewingMerchant; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        blackBackground.SetActive(false);
        patientScreen.gameObject.SetActive(false);
        patientScreenChart.gameObject.SetActive(false);
        alchemyCanvas.gameObject.SetActive(false);
        //merchantCanvas.gameObject.SetActive(false);
        dialogueCanvas.SetActive(false);

        if (cureResultText) cureResultText.gameObject.SetActive(false);
    }

    private void Update()
    {
        // temporary exiting for alchemy screen
        if (Input.GetKeyDown(KeyCode.Space) && viewingAlchemy)
        {
            ExitAlchemyScreen();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            EnterMerchantScreen();
        }
    }

    public void ViewPatientScreen(Sprite spr, string name, List<Disease.Symptom> symptoms, string info, DialogueScriptableObject dialogue)
    {
        if (!viewingPatient && !CheckPlayerIsViewingChart())
        {
            viewingPatient = true;
            PlayerController.Instance.SetPlayerMovement(false);
            blackBackground.SetActive(true);

            patientNameText.text = name;
            patientSymptomText.text = "- " + symptoms[0].symptomName;
            if (symptoms.Count > 1)
            {
                for (int i = 1; i < symptoms.Count; i++)
                {
                    patientSymptomText.text += "\n" + "- " + symptoms[i].symptomName;
                }
            }
            patientInfoText.text = info;
            patientScreenChart.gameObject.SetActive(true);
            patientScreen.sprite = spr;
            patientScreen.gameObject.SetActive(true);
            dialogueCanvas.SetActive(true); // Austin
            dialogueMaster.dialogue = dialogue;
            dialogueMaster.Startup(); // Austin
            JournalAndChart.Instance.ToggleButtons();
        }
    }

    public void ResetPatientScreen()
    {
        if (viewingPatient)
        {
            patientScreen.gameObject.SetActive(false);
            patientScreenChart.gameObject.SetActive(false);
            //patientScreenChart.sprite = null;
            blackBackground.SetActive(false);
            PlayerController.Instance.SetPlayerMovement(true);
            viewingPatient = false;
            dialogueCanvas.SetActive(false); // Austin
            JournalAndChart.Instance.ToggleButtons();
        }
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
}
