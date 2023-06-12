using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public List<string> symptomsTreated;
    public GameObject brewer;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        brewer = GameObject.FindGameObjectWithTag("Brewer");
    }

    // Update is called once per frame
    void Update()
    {
        if(brewer != null)
        {
            brewer.SendMessage("TransferSymptomsToPotion", this);
        }
    }
}
