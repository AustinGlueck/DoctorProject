using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private float moveSpeed = 5f;
    private bool canMove = true;
    public void SetPlayerMovement(bool b) { canMove = b; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && canMove)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && canMove)
        {
            transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
        }
    }
}
