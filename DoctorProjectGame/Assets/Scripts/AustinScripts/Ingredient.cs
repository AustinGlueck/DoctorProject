using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ingredient : MonoBehaviour
{
    public string ingredientName;
    public TextMeshProUGUI ingredientNameBox;
    public Image image;
    public Sprite rawSprite;
    public Sprite crushedSprite;
    public Sprite ruinedSprite;
    public enum IngredientState { Raw, Crushed, Boiled, Distilled, Radiated, Ruined};
    public IngredientState ingredientState;

    [Header("Boiled Symptoms")]
    public string[] boiledSymptomsTreated;
    public Sprite boiledSprite;

    [Header("Distilled Symptoms")]
    public string[] distilledSymptomsTreated;
    public Sprite distilledSprite;

    [Header("Radiated Symptoms")]
    public string[] radiatedSymptomsTreated;
    public Sprite radiatedSprite;
    // Start is called before the first frame update
    void Start()
    {
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateState()
    {
        ingredientNameBox.text = ingredientState + " " + ingredientName;

        if(ingredientState == IngredientState.Raw)
        {
            image.sprite = rawSprite;
        }
        if(ingredientState == IngredientState.Crushed)
        {
            image.sprite = crushedSprite;
        }
        if(ingredientState == IngredientState.Boiled)
        {
            image.sprite = boiledSprite;
        }
        if(ingredientState == IngredientState.Distilled)
        {
            image.sprite = distilledSprite;
        }
        if(ingredientState == IngredientState.Radiated)
        {
            image.sprite = radiatedSprite;
        }
        if(ingredientState == IngredientState.Ruined)
        {
            image.sprite = ruinedSprite;
        }
    }

    public void DestroyIngredient()
    {
        transform.parent.GetComponent<InventorySlot>().RemoveFromSlots();
        Destroy(gameObject);
    }

}
