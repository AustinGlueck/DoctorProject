using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAudio : MonoBehaviour
{
    public AudioSource keySoundQ;
    public AudioSource keySoundE;
    public AudioSource keySoundF;
    public AudioSource keySoundClick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            keySoundQ.Play();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            keySoundE.Play();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            keySoundF.Play();
        }
        if (Input.GetMouseButtonDown(0) == true)
        {
            keySoundClick.Play();
        }
    }
}

