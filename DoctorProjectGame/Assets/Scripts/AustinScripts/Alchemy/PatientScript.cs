using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientScript : MonoBehaviour
{
    public Disease disease;
    public DialogueScriptableObject dialogue;
    public string patientName;
    public List<Disease.Symptom> symptoms;
    private PatientGraphicsHandler patientGraphicsHandler;

    void Start()
    {
        patientGraphicsHandler = gameObject.GetComponent<PatientGraphicsHandler>();

        foreach(Disease.Symptom symptom in disease.symptoms)
        {
            symptoms.Add(symptom);
            if(symptom.diseaseGraphic != null)
            {
                if (symptom.symptomGraphicType == Disease.Symptom.SymptomGraphicType.none)
                {
                    patientGraphicsHandler.symptoms.Add(symptom.diseaseGraphic);
                }
                if(symptom.symptomGraphicType == Disease.Symptom.SymptomGraphicType.body)
                {
                    patientGraphicsHandler.bodyBase = symptom.diseaseGraphic;
                }
                if(symptom.symptomGraphicType == Disease.Symptom.SymptomGraphicType.eyes)
                {
                    patientGraphicsHandler.eyes = symptom.diseaseGraphic;
                }
                if(symptom.symptomGraphicType == Disease.Symptom.SymptomGraphicType.mouth)
                {
                    patientGraphicsHandler.mouth = symptom.diseaseGraphic;
                }
                if(symptom.symptomGraphicType == Disease.Symptom.SymptomGraphicType.nose)
                {
                    patientGraphicsHandler.nose = symptom.diseaseGraphic;
                }
            }
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
