using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpforce;
    public bool isJumping = false;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        spriteRenderer.flipX = true;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
        if (PauseMenu.isOn)
        {
            if (Cursor.lockState != CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            return;
        }

        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
       
        Flip(rb.velocity.x);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetBool("isJumping", isJumping);
        animator.SetFloat("Speed", characterVelocity);
        
    }

    void FixedUpdate()
    {
        
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        if (PauseMenu.isOn)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("isJumping", false);
            MovePlayer(0f);
            return;
        }
        MovePlayer(horizontalMovement);
        
    }

    void MovePlayer(float _horizontalMovement)
    {
        
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity,.05f);
        if (isJumping)
        {
            rb.AddForce(new Vector2(0f,jumpforce));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = true;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
