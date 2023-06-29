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
        merchantCanvas.gameObject.SetActive(false);
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

    public void ViewPatientScreen(Sprite spr, string name, string symptoms, string info)
    {
        if (!viewingPatient && !CheckPlayerIsViewingChart())
        {
            viewingPatient = true;
            PlayerController.Instance.SetPlayerMovement(false);
            blackBackground.SetActive(true);

            patientNameText.text = name;
            patientSymptomText.text = symptoms;
            patientInfoText.text = info;
            patientScreenChart.gameObject.SetActive(true);
            patientScreen.sprite = spr;
            patientScreen.gameObject.SetActive(true);
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
        if (!viewingMerchant)
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
        if (viewingMerchant)
        {
            merchantCanvas.gameObject.SetActive(false);

            blackBackground.SetActive(false);
            PlayerController.Instance.SetPlayerMovement(true);
            viewingMerchant = false;
        }
    }
}
