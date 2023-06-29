using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private bool isContact = false;

    //Patient
    public string patientName = "Joe";
    public string patientSymptoms = "symptoms";
    public string patientInfo = "info";
    [SerializeField] private bool hasPatient = false;
    private bool viewingPatient = false;

    //Chart
    [SerializeField] private bool haschart = false;
    [SerializeField] private SpriteRenderer bedChart;
    [SerializeField] private Sprite patientSprite;

    private void Start()
    {
        //chart.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isContact = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isContact = false;
            JournalAndChart.Instance.ToggleTooltipSpacebar(false);
            JournalAndChart.Instance.ToggleTooltipFkey(false);
        }
    }

    private bool IsContactWithPatient()
    {
        return isContact && hasPatient;
    }

    private void Update()
    {
        UpdateChartColor();
        UpdateTooltip();
        ViewPatient();
        PickUpChart();
    }

    private void UpdateChartColor()
    {
        if (isContact)
        {
            bedChart.color = !CheckPlayerIsHoldingChart() ? Color.grey : Color.white;
        }
        else
        {
            bedChart.color = Color.white;
        }
    }

    private void UpdateTooltip()
    {
        if (isContact)
        {
            if(CheckPlayerIsViewing())
            {
                JournalAndChart.Instance.ToggleTooltipSpacebar(false);
                JournalAndChart.Instance.ToggleTooltipFkey(false);
                return;
            }

            JournalAndChart.Instance.ToggleTooltipSpacebar(true);

            if (!haschart && CheckPlayerIsHoldingChart())
            {
                JournalAndChart.Instance.ToggleTooltipFkey(true);
                JournalAndChart.Instance.SetTooltipFkeyText("F Key: return chart");
            }
            else if(haschart && !CheckPlayerIsHoldingChart())
            {
                JournalAndChart.Instance.ToggleTooltipFkey(true);
                JournalAndChart.Instance.SetTooltipFkeyText("F Key: pick up chart");
            }
            else if(haschart && CheckPlayerIsHoldingChart())
            {
                JournalAndChart.Instance.ToggleTooltipFkey(false);
            }
        }
    }

    private void ViewPatient()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsContactWithPatient() && !CheckPlayerIsViewing())
        {
            if (!viewingPatient && !CheckCurrentlyViewingScreen())
            {
                viewingPatient = true;
                ScreenManager.Instance.ViewPatientScreen(patientSprite, patientName, patientSymptoms, patientInfo);
            }
            else if (viewingPatient && CheckCurrentlyViewingScreen())
            {
                viewingPatient = false;
                ScreenManager.Instance.ResetPatientScreen();
            }
        }
    }

    private void PickUpChart()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsContactWithPatient() && !CheckPlayerIsViewing() && !CheckCurrentlyViewingScreen())
        {
            if (haschart && !CheckPlayerIsHoldingChart())
            {
                haschart = false;
                JournalAndChart.Instance.SetHoldingChart(true);
                JournalAndChart.Instance.SetChartData(patientName, patientSymptoms, patientInfo);
                bedChart.gameObject.SetActive(false);
            }
            else if (!haschart && CheckPlayerIsHoldingChart())
            {
                haschart = true;
                JournalAndChart.Instance.SetHoldingChart(false);
                JournalAndChart.Instance.SetChartData(null,null,null);
                bedChart.gameObject.SetActive(true);
            }
        }
    }

    private bool CheckPlayerIsHoldingChart() { return JournalAndChart.Instance.GetHoldingChart(); }
    private bool CheckPlayerIsViewing() { return JournalAndChart.Instance.IsViewing(); }
    private bool CheckCurrentlyViewingScreen() { return ScreenManager.Instance.IsViewingScreen(); }
}
