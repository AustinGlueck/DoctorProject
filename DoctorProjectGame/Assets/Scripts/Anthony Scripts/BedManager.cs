using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedManager : MonoBehaviour
{
    //tempory for playtesting
    [System.Serializable]
    public class Patient
    {
        public Bed bed;
        public string name;
        [TextArea] public string symptoms;
        public string info;
    }

    public List<Patient> patients = new List<Patient>();

    private void Start()
    {
        foreach (Patient patient in patients)
        {
            UpdateBedPatient(patient);
        }
    }
    //

    //original method
    public void UpdateBedPatient(Bed bed, string name, string symptoms, string info)
    {
        bed.patientName = name;
        bed.patientSymptoms = symptoms;
        bed.patientInfo = info;
    }

    //streamlined method
    public void UpdateBedPatient(Patient patient)
    {
        patient.bed.patientName = patient.name;
        patient.bed.patientSymptoms = patient.symptoms;
        patient.bed.patientInfo = patient.info;
    }
}
