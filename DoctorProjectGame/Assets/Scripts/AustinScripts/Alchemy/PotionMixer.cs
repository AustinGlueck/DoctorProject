using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionMixer : MonoBehaviour
{
    public GameObject ingredient;
    public Ingredient ingredientScript;
    public PotionOutput potionOutput;
    // Start is called before the first frame update
    void Awake()
    {
        potionOutput = GameObject.FindGameObjectWithTag("Brewer").GetComponent<PotionOutput>();
    }


    public void NewIngredient(GameObject newIngredient)
    {
        ingredient = newIngredient;
        if (newIngredient != null)
        {
            ingredientScript = newIngredient.GetComponent<Ingredient>();
            potionOutput.ingredientScripts.Add(ingredientScript);
            potionOutput.ingredients.Add(ingredient);
        }
        else
        {
            potionOutput.ingredients.Remove(ingredient);
            potionOutput.ingredientScripts.Remove(ingredientScript);
            ingredientScript = null;
        }

    }
}
