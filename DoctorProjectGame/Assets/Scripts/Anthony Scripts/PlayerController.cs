using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private float timeTillIdle = 3f;
    private float currentTimeTillIdle = 0f;
    [SerializeField] private float moveSpeed = 5f;
    private bool canMove = true;
    public void SetPlayerMovement(bool b) { canMove = b; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        currentTimeTillIdle = timeTillIdle;
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 && canMove)
        {
            FlipSprite(false);
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            ResetIdleTime();
            SetWalkAnimation(true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && canMove)
        {
            FlipSprite(true);
            transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
            ResetIdleTime();
            SetWalkAnimation(true);
        }
        else
        {
            SetWalkAnimation(false);
            IdleAnimation();
        }
    }

    public void FlipSprite(bool b)
    {
        sprite.flipX = b;
    }

    private void IdleAnimation()
    {
        if (currentTimeTillIdle > 0)
        {
            currentTimeTillIdle -= Time.deltaTime;
        }
        else if (currentTimeTillIdle <= 0 && !animator.GetBool("Idle"))
        {
            animator.SetBool("Idle", true);
        }
    }

    private void ResetIdleTime()
    {
        currentTimeTillIdle = timeTillIdle;
        animator.SetBool("Idle", false);
    }

    private void SetWalkAnimation(bool b)
    {
        animator.SetBool("Walking", b);
    }
}
