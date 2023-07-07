using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalAndChart : MonoBehaviour
{
    public static JournalAndChart Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI tooltipSpacebarText;
    [SerializeField] private TextMeshProUGUI tooltipFkeyText;

    //Chart
    [SerializeField] private Image chartObject;
    [SerializeField] private TextMeshProUGUI chartObjectName;
    [SerializeField] private TextMeshProUGUI chartObjectSymptoms;
    [SerializeField] private TextMeshProUGUI chartObjectInfo;
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

    public void SetChartData(string name, List<Disease.Symptom> symptoms, string info)
    {
        if (name == null && symptoms == null && info == null)
        {
            chartObjectName.text = "";
            chartObjectSymptoms.text = "";
            chartObjectInfo.text = "";
            return;
        }

        chartObjectName.text = name;
        chartObjectSymptoms.text = "- " + symptoms[0].symptomName;
        if (symptoms.Count > 1)
        {
            for (int i = 1; i < symptoms.Count; i++)
            {
                chartObjectSymptoms.text += "\n" + "- " + symptoms[i].symptomName;
            }
        }
        chartObjectInfo.text = info;
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

    public void SetTooltipFkeyText(string str)
    {
        tooltipFkeyText.text = str;
    }

    public void ToggleTooltipFkey(bool b)
    {
        tooltipFkeyText.gameObject.SetActive(b);
    }

    public void ToggleTooltipSpacebar(bool b)
    {
        tooltipSpacebarText.gameObject.SetActive(b);
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
