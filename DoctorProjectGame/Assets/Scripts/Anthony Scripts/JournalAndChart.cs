using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalAndChart : MonoBehaviour
{
    public static JournalAndChart Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI tooltipFkeyText;
    [SerializeField] private TextMeshProUGUI tooltipGkeyText;

    //Chart
    [SerializeField] private Image chartObject;
    [SerializeField] private TextMeshProUGUI chartObjectName;
    [SerializeField] private GameObject patientSymptomsContent;
    [SerializeField] private TMP_InputField patientInputField;
    [SerializeField] private bool holdingChart = false;
    private bool viewingChart = false;
    public bool GetHoldingChart() { return holdingChart; }
    public void SetHoldingChart(bool b) { holdingChart = b; }

    [SerializeField] private GameObject chartButtonUI;
    private Image chartButtonImageUI;
    private Color chartButtonUIColor;
    private Color greyedOutColor;
    [SerializeField,Range(0,1)] private float greyedOutColorAlpha = 0.5f;

    //Journal
    [SerializeField] Image journalObject;
    private bool viewingJournal = false;

    [SerializeField] private GameObject journalButtonUI;

    //Both Chart and Journal
    public bool IsViewing() { return viewingJournal || viewingChart; }
    private bool canView = true;
    public void SetViewableState(bool b) { canView = b; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        chartObject.gameObject.SetActive(false);
        journalObject.gameObject.SetActive(false);

        chartButtonImageUI = chartButtonUI.GetComponentInChildren<Image>();
        chartButtonUIColor = chartButtonImageUI.color;
        greyedOutColor = chartButtonImageUI.color;
        greyedOutColor.a = greyedOutColorAlpha;
        chartButtonImageUI.color = greyedOutColor;
    }

    public void SetChartData(string name, List<bool> checkMarks, string info)
    {
        if (name == null && checkMarks == null && info == null)
        {
            chartObjectName.text = "";
            Toggle[] toggleList1 = patientSymptomsContent.GetComponentsInChildren<Toggle>();
            for (int i = 0; i < toggleList1.Length; i++)
            {
                toggleList1[i].isOn = false;
            }
            patientInputField.text = "";
            return;
        }

        chartObjectName.text = name;
        Toggle[] toggleList = patientSymptomsContent.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggleList.Length; i++)
        {
            toggleList[i].isOn = checkMarks[i];
        }
        patientInputField.text = info;
    }

    public void SaveToggleListToBed(Bed bed)
    {
        Toggle[] toggleList = patientSymptomsContent.GetComponentsInChildren<Toggle>();
        List<bool> newBoolList = new List<bool>();
        for (int tl = 0; tl < toggleList.Length; tl++)
        {
            newBoolList.Add(toggleList[tl].isOn);
        }

        bed.SetCheckMarks(newBoolList);
    }

    public void SaveNotes(Bed bed)
    {
        bed.SetNotes(patientInputField.text);
    }

    private void Update()
    {
        UpdateChartButton();
        
        KeyPressViewChart();
        KeyPressViewJournal();
    }

    private void UpdateChartButton()
    {
        chartButtonUI.GetComponent<Button>().interactable = holdingChart ? true : false;
        chartButtonImageUI.color = holdingChart ? chartButtonUIColor : greyedOutColor;
    }

    public void SetTooltipGkeyText(string str)
    {
        tooltipGkeyText.text = str;
    }

    public void ToggleTooltipGkey(bool b)
    {
        tooltipGkeyText.gameObject.SetActive(b);
    }

    public void ToggleTooltipFkey(bool b)
    {
        tooltipFkeyText.gameObject.SetActive(b);
    }

    private void KeyPressViewChart()
    {
        if (Input.GetKeyDown(KeyCode.E)) ViewChart();
    }

     public void ViewChart()
    {
        if (holdingChart && canView && !CheckCurrentlyViewingPatient())
        {
            viewingChart = !viewingChart;
            chartObject.gameObject.SetActive(viewingChart);
            PlayerController.Instance.SetPlayerMovement(!viewingChart && !viewingJournal);
        }
    }

    private void KeyPressViewJournal()
    {
        if (Input.GetKeyDown(KeyCode.Q)) ViewJournal();
    }

    public void ViewJournal()
    {
        if (canView && !CheckCurrentlyViewingPatient())
        {
            viewingJournal = !viewingJournal;
            journalObject.gameObject.SetActive(viewingJournal);
            PlayerController.Instance.SetPlayerMovement(!viewingChart && !viewingJournal);
        }
    }

    public void ToggleButtons()
    {
        chartButtonUI.SetActive(!chartButtonUI.activeSelf);
        journalButtonUI.SetActive(!journalButtonUI.activeSelf);
    }

    private bool CheckCurrentlyViewingScreen() { return ScreenManager.Instance.IsViewingScreen(); }
    private bool CheckCurrentlyViewingAlchemy() { return ScreenManager.Instance.IsViewingAlchemy(); }
    private bool CheckCurrentlyViewingPatient() { return ScreenManager.Instance.IsViewingPatient(); }
}
