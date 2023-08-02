using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientScript : MonoBehaviour
{
    public Disease disease;
    public DialogueScriptableObject dialogue;
    public string patientName;
    public List<Disease.Symptom> symptoms;

    void Start()
    {
        foreach(Disease.Symptom symptom in disease.symptoms)
        {
            symptoms.Add(symptom);
        }
    }

    public void CureDisease(Potion treatment)
    {
        if (treatment != null)
        {
            foreach (string symptomTreated in treatment.symptomsTreated)
            {
                for (int i = 0; i < symptoms.Count; i++)
                {
                    if (symptoms[i].symptomName == symptomTreated)
                    {
                        symptoms.Remove(symptoms[i]);
                        continue;
                    }
                }
            }

            if (symptoms.Count == 0)
            {
                print("cured");
                ScreenManager.Instance.DisplayCureResult(true);
                return;
            }
            ScreenManager.Instance.DisplayCureResult(false);
        }
    }
}
