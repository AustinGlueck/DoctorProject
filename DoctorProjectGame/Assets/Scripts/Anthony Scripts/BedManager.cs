using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviour
{
    public List<Bed> beds = new List<Bed>();
    public List<PatientScript> patients = new List<PatientScript>();

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
