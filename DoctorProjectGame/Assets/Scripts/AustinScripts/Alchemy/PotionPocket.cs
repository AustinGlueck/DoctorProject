using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionPocket : MonoBehaviour
{
    public static PotionPocket Instance { get; private set; }
    public GameObject potion;
    public Potion potionScript;
    public PatientScript targetPatient;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewPotion(GameObject newPotion)
    {
        potion = newPotion;
        if (newPotion != null)
        {
            potionScript = newPotion.GetComponent<Potion>();
        }
        else
        {
            potionScript = null;
        }
    }

    public void ApplyCure()
    {
        print("ApplyCure");
        targetPatient.CureDisease(potionScript);
        Destroy(potion);
        NewPotion(null);
    }
}
