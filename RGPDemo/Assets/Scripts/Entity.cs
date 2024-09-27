using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;
    [Header("CollisionInfo")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGrount;
    [SerializeField] protected Transform groundCheck;
    protected bool isGround;
    protected int facingDir = 1;
    protected bool facingRight = true;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        ColissionChecks();
    }
    /// <summary>
    /// check isground  
    /// </summary>
    private void ColissionChecks()
    {
        isGround = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGrount);
    }
    /// <summary> 
    /// flip the sprite
    /// </summary>
    protected virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    /// <summary>
    /// draw a line between player and ground
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }
}
