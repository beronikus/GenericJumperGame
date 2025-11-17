using System;
using UnityEngine;
using UnityEngine.Timeline;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public EventHandler OnReachingGoal;
    public EventHandler OnDeath;
    public EventHandler OnJump;

    [SerializeField] private float playerMovementSpeed = 1f;
    [SerializeField] private float jumpHeigh = 1f;
    [SerializeField] private float rayOffset = 0.05f;
    [SerializeField] private float raycastLenght = 0.1f;
    [SerializeField] private float coyoteTime = 0.3f;
    [SerializeField] private float coyoteTimeDuration;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
  

    private Vector2 moveInput;
    private Rigidbody2D rb2D;
    private BoxCollider2D boxCollider2D;
  
    

    private void Awake()
    {
        Instance = this;
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

  

    private void FixedUpdate()
    {
        moveInput = PlayerInputAction.Instance.MoveActionPressed();
        rb2D.linearVelocity = new Vector2(moveInput.x * playerMovementSpeed, rb2D.linearVelocity.y);
       
        animator.SetBool("isRunning", Mathf.Abs(moveInput.x) > 0.01f);

        if (moveInput.x > 0.01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (moveInput.x < -0.01f)
        {
            spriteRenderer.flipX = true;
        }
        
        if (GetIsGrounded())
        {
            coyoteTimeDuration = coyoteTime;
        }
        else
        {
            coyoteTimeDuration -= Time.fixedDeltaTime;
        }

        if (PlayerInputAction.Instance.JumpActionPresssed() && coyoteTimeDuration > 0f)
        {
            
            rb2D.linearVelocityY = jumpHeigh;
            coyoteTimeDuration = 0;
            OnJump?.Invoke(this, EventArgs.Empty);
            
        }
        
        

       
    }

    // Check ob Character auf den Boden ist.
    private bool GetIsGrounded()
    {
        Vector2 rayOrigin = new Vector2(transform.position.x, boxCollider2D.bounds.min.y + rayOffset);
        RaycastHit2D raycastDownHit2D = Physics2D.Raycast(rayOrigin, Vector2.down, raycastLenght, groundMask);
        Vector2 raycastDirection = Vector2.down * raycastLenght;
        
        Debug.DrawRay(rayOrigin, raycastDirection, Color.blue );

        return raycastDownHit2D.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out Goal goal))
        {
            Debug.Log("Reached Goal");
            OnReachingGoal?.Invoke(this, EventArgs.Empty);
            
        } ;
    }

  
   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out Hazards hazard))
        {
            Destroy(gameObject);
            OnDeath?.Invoke(this, EventArgs.Empty);
        }
    }


    
}
