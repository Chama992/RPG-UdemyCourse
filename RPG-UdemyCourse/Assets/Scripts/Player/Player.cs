using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public bool IsBusy { get; private set; }

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


    protected override void Awake()
    {
        base.Awake();
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

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }
    protected override void Update()
    {
        base.Update();
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
}
