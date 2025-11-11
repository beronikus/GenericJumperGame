using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    [SerializeField] private float playerMovementSpeed = 1f;
    [SerializeField] private float jumpHeigh = 1f;
    private Vector2 moveInput;
    private Rigidbody2D rb2D;
    private bool isJumping = false;
    

    private void Awake()
    {
        Instance = this;
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        moveInput = PlayerInputAction.Instance.MoveActionPressed();
        rb2D.linearVelocity = new Vector2(moveInput.x * playerMovementSpeed, rb2D.linearVelocity.y);

        if (PlayerInputAction.Instance.JumpActionPresssed() && isJumping == false)
        {
            rb2D.linearVelocityY = jumpHeigh;
        }
        
    }

    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJumping = false;
            Debug.Log("Touching the Ground");
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isJumping = true;
            Debug.Log("Jumping");
        }
    }
}
