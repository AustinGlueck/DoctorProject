using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform relocateTransform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.position = relocateTransform.position;
            PlayerController.Instance.FlipSprite(false);
            // Load Alchemy Scene
            ScreenManager.Instance.EnterAlchemyScreen();
        }
    }
}
