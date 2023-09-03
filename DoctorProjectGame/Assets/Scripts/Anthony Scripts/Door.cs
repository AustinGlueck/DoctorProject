using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform relocateTransform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && TutorialCheck())
        {
            collision.gameObject.transform.position = relocateTransform.position;
            PlayerController.Instance.FlipSprite(false);
            // Load Alchemy Scene
            ScreenManager.Instance.EnterAlchemyScreen();
        }
    }

    private bool TutorialCheck()
    {
        if (MainTutorial.Instance != null)
        {
            return MainTutorial.Instance.enableTutorial && MainTutorial.Instance.currentStage == 4;
        }
        else
        {
            return true;
        }
    }
}
