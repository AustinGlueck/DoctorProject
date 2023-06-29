using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public List<string> symptomsTreated;
    public GameObject brewer;
    public Sprite ruinedSprite;
    private Image image;
    public bool ruined;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        brewer = GameObject.FindGameObjectWithTag("Brewer");
        image = gameObject.GetComponent<Image>();


        /*if (ruined)
        {
            ChangeSprite();
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (brewer != null && !ruined)
        {
            brewer.SendMessage("TransferSymptomsToPotion", this);
        }
        if(brewer != null && ruined)
        {
            brewer.SendMessage("ClearIngredients", this);
        }
    }

    public void ChangeSprite()
    {
        image.sprite = ruinedSprite;
    }
}
