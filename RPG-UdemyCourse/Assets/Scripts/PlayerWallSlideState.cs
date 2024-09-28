using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0 && player.facingDir != xInput)
            StateMachine.ChangeState(player.IdleState);
        if (yInput != 0)
            rb.velocity = new Vector2(0, rb.velocity.y * player.WallSlideYFastSpeedCoefficient);
        else 
            rb.velocity = new Vector2(0, rb.velocity.y * player.WallSlideYSlowSpeedCoefficient);
        if (player.IsGroundChecked())
            StateMachine.ChangeState(player.IdleState);
    }
}
