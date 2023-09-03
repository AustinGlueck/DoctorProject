using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientGraphicsHandler : MonoBehaviour
{
    public GameObject patientScreen;
    public GameObject bodyBase;
    public GameObject hair;
    public GameObject eyes;
    public GameObject nose;
    public GameObject mouth;
    public List<GameObject> symptoms;
    public List<GameObject> activeSymptomGraphics;

    private GameObject newBaseBody;
    private GameObject newNose;
    private GameObject newHair;
    private GameObject newMouth;

    public void InstantiateCharacter()
    {
        foreach (GameObject symptomGraphic in symptoms)
        {
            GameObject newSymptom = Instantiate(symptomGraphic, patientScreen.transform);
            activeSymptomGraphics.Add(newSymptom);
            newSymptom.transform.SetAsFirstSibling();
        }

        newBaseBody = Instantiate(bodyBase, patientScreen.transform);
        //Instantiate(eyes);
        newNose = Instantiate(nose, patientScreen.transform);
        newHair = Instantiate(hair, patientScreen.transform);
        newMouth = Instantiate(mouth, patientScreen.transform);

        newHair.transform.SetAsFirstSibling();
        newNose.transform.SetAsFirstSibling();
        newBaseBody.transform.SetAsFirstSibling();

    }

    public void DestroyGraphics()
    {
        Destroy(newBaseBody);
        Destroy(newNose);
        Destroy(newHair);
        Destroy(newMouth);

        foreach (GameObject graphic in activeSymptomGraphics)
        {
            int activeIndex = activeSymptomGraphics.IndexOf(graphic);
            Destroy(graphic);
            activeSymptomGraphics.RemoveAt(activeIndex);
        }
    }
}
