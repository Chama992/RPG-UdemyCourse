using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.wallJumpDuration;
        player.SetVelocity(player.wallJumpXMoveCoefficient * -player.facingDir, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer < 0)
            StateMachine.ChangeState(player.AirState);
        if (player.IsGroundChecked())
            StateMachine.ChangeState(player.IdleState);
        base.Update();
    }
}
