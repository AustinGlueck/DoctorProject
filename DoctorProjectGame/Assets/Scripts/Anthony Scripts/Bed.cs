using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private bool isContact = false;

    //Patient
    [SerializeField] private PatientScript patient;
    public void SetPatient(PatientScript patient) { this.patient = patient; }
    private List<bool> checkMarks = new List<bool>();
    private int maxSymptoms = 3;//revisit to streamline
    public void SetCheckMarks(List<bool> checkMarks) { this.checkMarks = checkMarks; }
    public string notes = "info"; //revisit
    [SerializeField] private bool hasPatient = false;

    //Chart
    [SerializeField] private bool haschart = false;
    [SerializeField] private SpriteRenderer bedChart;
    [SerializeField] private Sprite patientSprite;

    private void Start()
    {
        //chart.SetActive(false);
        RestCheckMarks();
    }

    private void RestCheckMarks()
    {
        checkMarks.Clear();
        for (int i=0; i<maxSymptoms; i++)
        {
            checkMarks.Add(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isContact = true;
            PotionPocket.Instance.targetPatient = patient;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isContact = false;
            JournalAndChart.Instance.ToggleTooltipFkey(false);
            JournalAndChart.Instance.ToggleTooltipGkey(false);
            PotionPocket.Instance.targetPatient = null;
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
                JournalAndChart.Instance.ToggleTooltipFkey(false);
                JournalAndChart.Instance.ToggleTooltipGkey(false);
                return;
            }

            JournalAndChart.Instance.ToggleTooltipFkey(true);

            if (!haschart && CheckPlayerIsHoldingChart())
            {
                JournalAndChart.Instance.ToggleTooltipGkey(true);
                JournalAndChart.Instance.SetTooltipGkeyText("G Key: return chart");
            }
            else if(haschart && !CheckPlayerIsHoldingChart())
            {
                JournalAndChart.Instance.ToggleTooltipGkey(true);
                JournalAndChart.Instance.SetTooltipGkeyText("G Key: pick up chart");
            }
            else if(haschart && CheckPlayerIsHoldingChart())
            {
                JournalAndChart.Instance.ToggleTooltipGkey(false);
            }
        }
    }

    private void ViewPatient()
    {
        if (Input.GetKeyDown(KeyCode.F) && IsContactWithPatient() && !CheckPlayerIsViewing())
        {
            if (!CheckCurrentlyViewingScreen())
            {
                ScreenManager.Instance.ViewPatientScreen(patientSprite, patient.patientName, checkMarks, notes, patient.dialogue);
                BedManager.Instance.SetActiveBed(this);
            }
            else if (CheckCurrentlyViewingScreen())
            {
                ScreenManager.Instance.ResetPatientScreen();
            }
        }
    }

    private void PickUpChart()
    {
        if (Input.GetKeyDown(KeyCode.G) && IsContactWithPatient() && !CheckPlayerIsViewing() && !CheckCurrentlyViewingScreen())
        {
            if (haschart && !CheckPlayerIsHoldingChart())
            {
                haschart = false;
                JournalAndChart.Instance.SetHoldingChart(true);
                JournalAndChart.Instance.SetChartData(patient.patientName, patient.symptoms, notes);
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
