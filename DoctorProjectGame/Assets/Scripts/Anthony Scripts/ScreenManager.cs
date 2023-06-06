using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    [SerializeField] private GameObject blackBackground;
    [SerializeField] private Image patientScreenChart;
    [SerializeField] private TextMeshProUGUI patientScreenChartText;
    private bool viewingScreen = false;
    public bool IsViewingScreen() { return viewingScreen; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        blackBackground.SetActive(false);
        patientScreenChart.gameObject.SetActive(false);
    }

    public void ViewPatientScreen(string str)
    {
        if (!viewingScreen && !CheckPlayerIsViewingChart())
        {
            viewingScreen = true;
            PlayerController.Instance.SetPlayerMovement(false);
            blackBackground.SetActive(true);
            patientScreenChartText.text = str;
            patientScreenChart.gameObject.SetActive(true);
            JournalAndChart.Instance.ToggleButtons();
        }
    }

    public void ResetPatientScreen()
    {
        if (viewingScreen)
        {
            patientScreenChart.gameObject.SetActive(false);
            //patientScreenChart.sprite = null;
            blackBackground.SetActive(false);
            PlayerController.Instance.SetPlayerMovement(true);
            viewingScreen = false;
            JournalAndChart.Instance.ToggleButtons();
        }
    }

    public void EnterAlchemyScreen()
    {
        if (!viewingScreen)
        {
            PlayerController.Instance.SetPlayerMovement(false);
            viewingScreen = true;
            JournalAndChart.Instance.ToggleButtons();
            blackBackground.SetActive(true);
        }
    }

    public void ExitAlchemyScreen()
    {
        if (viewingScreen)
        {
            blackBackground.SetActive(false);
            JournalAndChart.Instance.ToggleButtons();
            viewingScreen = false;
            PlayerController.Instance.SetPlayerMovement(true);
        }
    }

    private bool CheckPlayerIsViewingChart() { return JournalAndChart.Instance.IsViewing(); }
}
