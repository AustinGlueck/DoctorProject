using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private bool isContact = false;

    //Patient
    public string patientName = "Joe";
    [SerializeField] private bool hasPatient = false;
    private bool viewingPatient = false;

    //Chart
    [SerializeField] private bool haschart = false;
    [SerializeField] private GameObject bedChart;
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
        }
    }

    private bool IsContactWithPatient()
    {
        return isContact && hasPatient;
    }

    private void Update()
    {
        ViewPatient();
        PickUpChart();
    }

    private void ViewPatient()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsContactWithPatient() && !CheckPlayerIsViewingChart())
        {
            print(Screen.width);
            if (!viewingPatient && !CheckCurrentlyViewingScreen())
            {
                viewingPatient = true;
                ScreenManager.Instance.ViewPatientScreen(patientName);
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
        if (Input.GetKeyDown(KeyCode.P) && IsContactWithPatient() && !CheckPlayerIsViewingChart() && !CheckCurrentlyViewingScreen())
        {
            if (haschart && !CheckPlayerIsHoldingChart())
            {
                haschart = false;
                JournalAndChart.Instance.SetHoldingChart(true);
                JournalAndChart.Instance.SetChartData(patientName);
                bedChart.SetActive(false);
            }
            else if (!haschart && CheckPlayerIsHoldingChart())
            {
                haschart = true;
                JournalAndChart.Instance.SetHoldingChart(false);
                JournalAndChart.Instance.SetChartData(null);
                bedChart.SetActive(true);
            }
        }
    }

    private bool CheckPlayerIsHoldingChart() { return JournalAndChart.Instance.GetHoldingChart(); }
    private bool CheckPlayerIsViewingChart() { return JournalAndChart.Instance.IsViewing(); }
    private bool CheckCurrentlyViewingScreen() { return ScreenManager.Instance.IsViewingScreen(); }
}
