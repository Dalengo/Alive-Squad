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

    private bool facingRight = true;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public Canvas healthBarPlate;

    private Vector3 velocity = Vector3.zero;
    
    void Start()
    {
        spriteRenderer.flipX = facingRight;
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
        if (horizontalMovement>0.1f && facingRight || horizontalMovement < -0.1f && !facingRight)
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

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    

    // invoked by clients but executed on the server only
    [Command]
    void CmdProvideFlipStateToServer(bool state)
    {
        // make the change local on the server
        spriteRenderer.flipX = state;

        // forward the change also to all clients
        RpcSendFlipState(state);
    }

    // invoked by the server only but executed on ALL clients
    [ClientRpc]
    void RpcSendFlipState(bool state)
    {
        // skip this function on the LocalPlayer 
        // because he is the one who originally invoked this
        if (isLocalPlayer) return;

        //make the change local on all clients
        spriteRenderer.flipX = state;
    }

    // Client makes sure this function is only executed on clients
    // If called on the server it will throw an warning
    // https://docs.unity3d.com/ScriptReference/Networking.ClientAttribute.html
    [Client]
    private void Flip()
    {
        //Only go on for the LocalPlayer
        if (!isLocalPlayer) return;

        // make the change local on this client
        facingRight = !facingRight;
        spriteRenderer.flipX = !facingRight;

        // invoke the change on the Server as you already named the function
        CmdProvideFlipStateToServer(spriteRenderer.flipX);
    }

}
