using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disease : MonoBehaviour
{
    public string diseaseName;
    public int sicknessLevel;

    public Symptom[] symptoms;
    [System.Serializable]
    public class Symptom
    {
        public string symptomName;
        public int symptomStrength;
    }
    // Start is called before the first frame update
    void Start()
    {
        foreach(Symptom symptom in symptoms)
        {
            sicknessLevel += symptom.symptomStrength;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
