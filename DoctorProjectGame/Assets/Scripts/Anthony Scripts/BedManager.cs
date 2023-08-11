using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviour
{
    public static BedManager Instance { get; private set; }
    public List<Bed> beds = new List<Bed>();
    public List<PatientScript> patients = new List<PatientScript>();
    private Bed currentBed = null;
    public void SetActiveBed(Bed bed) { currentBed = bed; }
    public Bed GetActiveBed() { return currentBed; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        for (int i=0; i<beds.Count; i++)
        {
            if (patients[i] == null) break;
            UpdateBedPatient(beds[i], patients[i]);
        }
    }
    
    public void UpdateBedPatient(Bed bed, PatientScript patient)
    {
        bed.SetPatient(patient);
    }
}
