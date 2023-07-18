using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarAndPestle : MonoBehaviour
{
    public GameObject ingredient;
    public Ingredient ingredientScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PulverizeIngredient()
    {
        if(ingredient != null && ingredientScript != null)
        {
            if(ingredientScript.ingredientState == Ingredient.IngredientState.Raw)
            {
                ingredientScript.ingredientState = Ingredient.IngredientState.Crushed;
            }
            else
            {
                ingredientScript.ingredientState = Ingredient.IngredientState.Ruined;
            }
            ingredientScript.UpdateState();
        }

    }

    public void NewIngredient(GameObject newIngredient)
    {
        ingredient = newIngredient;
        if(newIngredient != null)
        {
            ingredientScript = newIngredient.GetComponent<Ingredient>();
        }
        else
        {
            ingredientScript = null;
        }
    }
}
