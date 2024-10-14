using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public EntityFX FlashFX { get; private set; }
    #endregion
    [Header("CollisionCheck Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] public Transform AttackCheck;
    [SerializeField] public float AttackCheckRadius;
    #region FacingDir
    public int facingDir { get; private set; } = 1;
    public bool facingRight { get; private set; } = true;
    #endregion
    #region Knock
    [Header("Knock Info")]
    public Vector2 knockForce;
    public float knockBackDuration;
    private bool isKnocked;
    #endregion
    protected virtual void Awake()
    {
        
    }
    // Start is called before the first frame update
    protected virtual  void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        FlashFX = GetComponent<EntityFX>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    #region Collision
    /// <summary>
    /// check if player on the ground
    /// </summary>
    /// <returns></returns>
    public virtual bool IsGroundChecked() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    /// <summary>
    /// check if player near or on the wall
    /// </summary>
    /// <returns></returns>
    public virtual bool IsWallChecked() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    /// <summary>
    /// draw two line, one for groundcheck ,another for wallcheck
    /// </summary>
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(AttackCheck.position, AttackCheckRadius);
    }
    #endregion
    #region Flip
    /// <summary>
    /// just like what the method'name ,just flip
    /// </summary>
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    /// <summary>
    /// check if player really need to flip
    /// </summary>
    /// <param name="_x"></param>
    protected virtual void FlipControl(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }
    #endregion
    #region Velocity
    /// <summary>
    /// change player's velocity
    /// </summary>
    /// <param name="_xVelocity"></param>
    /// <param name="_yVelocity"></param>
    public virtual void SetVelocity(float _xVelocity, float _yVelocity)
    {
        if (isKnocked)
            return;
        Rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipControl(_xVelocity);
    }
    #endregion
    public virtual void GetDamage()
    {
        FlashFX.StartCoroutine("FlashFX");
        StartCoroutine("KnockBack");
    }
    protected virtual IEnumerator KnockBack()
    {
        isKnocked = true;
        Rb.velocity = new Vector2(knockForce.x * -facingDir, knockForce.y);
        yield return new WaitForSeconds(knockBackDuration);
        isKnocked = false;
    }
}
