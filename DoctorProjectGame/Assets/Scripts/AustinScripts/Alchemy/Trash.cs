using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public GameObject newObject;

    public void TrashObject()
    {
        Destroy(newObject);
        newObject = null;
    }
}
