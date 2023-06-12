using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionOutput : MonoBehaviour
{
    public InventorySlot inventorySlot;
    public List<GameObject> ingredients;
    public List<Ingredient> ingredientScripts;

    public List<string> finalSymptoms;
    public GameObject newPotion;
    // Start is called before the first frame update
    void Start()
    {

        inventorySlot = gameObject.GetComponent<InventorySlot>();
    }

    // Update is called once per frame


    public void MixPotion()
    {
        foreach(Ingredient ingredientScript in ingredientScripts)
        {
            if(ingredientScript.ingredientState == Ingredient.IngredientState.Boiled)
            {
                foreach(string symptomTreated in ingredientScript.boiledSymptomsTreated)
                {
                    finalSymptoms.Add(symptomTreated);
                }
            }

            if (ingredientScript.ingredientState == Ingredient.IngredientState.Distilled)
            {
                foreach (string symptomTreated in ingredientScript.distilledSymptomsTreated)
                {
                    finalSymptoms.Add(symptomTreated);
                }
            }

            if (ingredientScript.ingredientState == Ingredient.IngredientState.Radiated)
            {
                foreach (string symptomTreated in ingredientScript.radiatedSymptomsTreated)
                {
                    finalSymptoms.Add(symptomTreated);
                }
            }
        }

        Instantiate(newPotion,gameObject.transform);
    }

    public void TransferSymptomsToPotion(Potion potion)
    {
        foreach(string symptom in finalSymptoms)
        {
            potion.symptomsTreated.Add(symptom);
            finalSymptoms.Remove(symptom);
        }
        foreach(Ingredient ingredientScript in ingredientScripts)
        {
            ingredientScript.DestroyIngredient();
        }
        potion.brewer = null;
    }
}
