using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionOutput : MonoBehaviour
{
    [HideInInspector]public InventorySlot inventorySlot;
    public List<GameObject> ingredients;
    public List<Ingredient> ingredientScripts;

    public List<string> finalSymptoms;
    public GameObject newPotion;
    //private GameObject instantiatedPotion;
    // Start is called before the first frame update
    void Start()
    {

        inventorySlot = gameObject.GetComponent<InventorySlot>();
    }

    // Update is called once per frame


    public void MixPotion()
    {
        if(ingredientScripts.Count > 0)
        {
            GameObject instantiatedPotion;
            bool potionRuined = false;
            if (inventorySlot.transform.childCount > 0)
            {
                Destroy(inventorySlot.transform.GetChild(0).gameObject);
            }
            foreach (Ingredient ingredientScript in ingredientScripts)
            {
                if (ingredientScript.ingredientState == Ingredient.IngredientState.Boiled)
                {
                    foreach (string symptomTreated in ingredientScript.boiledSymptomsTreated)
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

                if (ingredientScript.ingredientState == Ingredient.IngredientState.Raw || ingredientScript.ingredientState == Ingredient.IngredientState.Ruined)
                {
                    potionRuined = true;
                }
            }

            instantiatedPotion = Instantiate(newPotion, gameObject.transform);

            if (potionRuined)
            {
                RuinPotion(instantiatedPotion);
            }
        }
    }

    private void RuinPotion(GameObject potion)
    {
        Potion potionScript = potion.GetComponent<Potion>();
        potionScript.ChangeSprite();
        potionScript.ruined = true;

        foreach (string sypmtom in finalSymptoms)
        {
            finalSymptoms.Remove(sypmtom);
        }

    }

    public void ClearIngredients(Potion potion)
    {
        foreach (Ingredient ingredientScript in ingredientScripts)
        {
            ingredientScript.DestroyIngredient();
        }
        potion.brewer = null;
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
