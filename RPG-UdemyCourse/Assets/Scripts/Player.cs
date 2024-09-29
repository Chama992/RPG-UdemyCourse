using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool IsBusy { get; private set; }
    #region Components
    public Animator Anim { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    #endregion
    #region PlayerStates
    public PlayerStateMachine StateMachine { get; private set;  }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerAirState AirState { get; private set;}
    public PlayerJumpState JumpState { get; private set; }
    public PlayerDashState DashState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }
    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }
    #endregion
    [Header("Move Info")]
    [SerializeField] public float moveSpeed;
    [Header("JumpFall Info")]
    [SerializeField] public float jumpForce;
    [SerializeField] public float airMoveSpeed;
    [Header("WallSlide Info")]
    [SerializeField] public float wallSlideYSlowSpeedCoefficient;
    [SerializeField] public float wallSlideYFastSpeedCoefficient;
    [SerializeField] public float wallJumpXMoveSpeed;
    [SerializeField] public float wallJumpDuration;
    [Header("Dash Info")]
    [SerializeField] public float dashDuration;
    [SerializeField] public float dashSpeed;
    [SerializeField] private float dashCoolDown;
    private float dashUsageTimer;
    [Header("Attack Info")]
    [SerializeField] public Vector2[] attackMove;
    public float dashDir { get; private set; }
    [Header("CollisionCheck Info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    public int facingDir { get; private set;  } = 1;
    public bool facingRight { get; private set; } = true;
    private void Awake()
    {
        ///use statemachine,player can switch to any state
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
        AirState = new PlayerAirState(this, StateMachine,"Jump");
        JumpState = new PlayerJumpState(this, StateMachine, "Jump");
        DashState = new PlayerDashState(this, StateMachine, "Dash");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, "Jump");
        PrimaryAttackState = new PlayerPrimaryAttackState(this, StateMachine, "Attack");
    }

    private void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.currentState.Update();
        CheckDashActive();
    }
    /// <summary>
    /// make player busy,used to not let player to do other thing 
    /// </summary>
    /// <param name="_seconds"></param>
    /// <returns></returns>
    public IEnumerator BusyFor(float _seconds)
    {
        IsBusy = true;
        yield return new WaitForSeconds(_seconds);
        IsBusy = false;
    }
    #region Collision
    /// <summary>
    /// check if player on the ground
    /// </summary>
    /// <returns></returns>
    public bool IsGroundChecked() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    /// <summary>
    /// check if player near or on the wall
    /// </summary>
    /// <returns></returns>
    public bool IsWallChecked() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    #endregion
    #region Velocity
    /// <summary>
    /// change player's velocity
    /// </summary>
    /// <param name="_xVelocity"></param>
    /// <param name="_yVelocity"></param>
    public void SetVelocity( float _xVelocity, float _yVelocity)
    {
        Rb.velocity =new Vector2(_xVelocity, _yVelocity);
        FlipControl(_xVelocity);
    }
    /// <summary>
    /// remove the player's velocity Don't mOVE 
    /// </summary>
    public void ZeroVelocity() => Rb.velocity = Vector2.zero;
    #endregion
    #region Flip
    /// <summary>
    /// just like what the method'name ,just flip
    /// </summary>
    private void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    /// <summary>
    /// check if player really need to flip
    /// </summary>
    /// <param name="_x"></param>
    private void FlipControl(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if ( _x < 0 && facingRight)
            Flip();
    }
    #endregion
    /// <summary>
    /// dash has the highest priority,so this method born
    /// </summary>
    public void CheckDashActive()
    {
        if (this.IsWallChecked()) //near wall can't dash
            return;
        dashUsageTimer -= Time.deltaTime;//use for cooldown
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0) // press lshift and is't cooldown
        {
            dashUsageTimer = dashCoolDown;
            // dash to what i press ,if not pressed dash to facingdir
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
            if (dashDir != facingDir)
                Flip();
            StateMachine.ChangeState(this.DashState);
        }
    }
    /// <summary>
    /// use for check the currentstate anim finished?
    /// </summary>
    public void AnimationTrigger() => this.StateMachine.currentState.AnimationFinishTrigger();
    /// <summary>
    /// draw two line, one for groundcheck ,another for wallcheck
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
}
