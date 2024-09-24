using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private Animator animator;
    private float xInput;
    private int facingDir = 1;
    private bool facingRight = true;
    [Header("Moving")]
    [SerializeField]private float jumpForce;
    [SerializeField]private float moveSpeed;
    private bool isGround;
    [Header("CollisionInfo")]
    [SerializeField]private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGrount;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        MoveMent();
        ColissionChecks();
        FlipController();
        AnimatorController();

    }
    /// <summary>
    /// check isground
    /// </summary>
    private void ColissionChecks()
    {
        isGround = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGrount);
    }

    /// <summary>
    /// check input
    /// </summary>
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }
    /// <summary>
    /// change x volocity
    /// </summary>
    private void MoveMent()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);
    }
    /// <summary>
    /// jump,make y a force 
    /// </summary>
    private void Jump()
    {
        if (isGround)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    /// <summary>
    /// controll the idle and move animation
    /// </summary>
    private void AnimatorController()
    {
        bool isMoving = xInput != 0;
        animator.SetBool("isMoving", isMoving);
    }
    /// <summary>
    /// flip the sprite
    /// </summary>
    private void Flip()
    {
        facingDir = facingDir = -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    /// <summary>
    /// controll the flip 
    /// </summary>
    private void FlipController()
    {
        if (rb.velocity.x > 0 && !facingRight)        
            Flip();
        else if (rb.velocity.x < 0 && facingRight)        
            Flip();        
    }
    /// <summary>
    /// draw a line between player and ground
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
