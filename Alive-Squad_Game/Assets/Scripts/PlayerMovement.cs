using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed;
    public float jumpforce;
    public bool isJumping = false;
    public bool isGrounded;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask collisionLayers;

    private bool flipped = false;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Canvas healthBarPlate;

    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        spriteRenderer.flipX = true;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayers);
       

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isJumping = true;
        }
       
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
        if (horizontalMovement>0.1f && flipped || horizontalMovement < -0.1f && !flipped)
        {
            Flip();
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

    void Flip()
    {
        Vector3 currentscale = gameObject.transform.localScale;
        currentscale.x *= -1;
        gameObject.transform.localScale = currentscale;
        currentscale = healthBarPlate.transform.localScale;
        currentscale.x *= -1;
        healthBarPlate.transform.localScale = currentscale;
        flipped = !flipped;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
