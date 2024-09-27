using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerOld : Entity
{
    // Start is called before the first frame update

    private float xInput;

    [Header("MovingInfo")]
    [SerializeField]private float jumpForce;
    [SerializeField]private float moveSpeed;
    [Header("DashInfo")]
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashCoolDownTimer;
    [Header("Attack Info")]
    [SerializeField] private float comboTime = .3f;
    private float comboTimeWindow;
    private bool isAttacking;
    private int attackCombo;
    protected override void Start()
    { 
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        MoveMent();
        CheckInput();
        dashTime -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (dashCoolDownTimer < 0)
                DashAbility();
        }
        FlipController();
        AnimatorController();
    }

    private void DashAbility()
    {
        if (!isAttacking)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashDuration;
        }
    }
    /// <summary>
    /// check input
    /// </summary>
    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetMouseButtonDown(0))
        {
            AttackEvent();
        }
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void AttackEvent()
    {
        if (!isGround)
            return;
        if (comboTimeWindow < 0)
            attackCombo = 0;
        comboTimeWindow = comboTime;
        isAttacking = true;
    }

    /// <summary>
    /// change x volocity
    /// </summary>
    private void MoveMent()
    {
        if (isAttacking)
            rb.velocity = Vector2.zero;
        else if (dashTime > 0)
            rb.velocity = new Vector2(facingDir * dashSpeed, 0);
        else
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
        animator.SetFloat("yVelocity", rb.velocity.y);
        //Debug.Log(rb.velocity.y);
        animator.SetBool("isMoving", isMoving);
        animator.SetBool("isGround", isGround);
        animator.SetBool("isDashing", dashTime > 0);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("attackCombo", attackCombo);
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
    public void QuitAttack()
    {
        attackCombo++;
        if (attackCombo > 2)
            attackCombo = 0;
        isAttacking = false;
    }
}
