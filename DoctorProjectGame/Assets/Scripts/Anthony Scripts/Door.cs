using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform relocateTransform;

    private void Update()
    {
        // temporary exiting for alchemy screen
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ScreenManager.Instance.ExitAlchemyScreen();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = relocateTransform.position;
            // Load Alchemy Scene
            ScreenManager.Instance.EnterAlchemyScreen();
        }
    }
}
