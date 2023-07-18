using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Disease", menuName = "Disease / New Disease")]
public class Disease : ScriptableObject
{

    public string diseaseName;
    //public int sicknessLevel;

    public Symptom[] symptoms;
    [System.Serializable]
    public class Symptom
    {
        public string symptomName;
        public int symptomStrength;
    }
    // Start is called before the first frame update
    /*void Start()
    {
        foreach(Symptom symptom in symptoms)
        {
            sicknessLevel += symptom.symptomStrength;
        }
    }*/

}
