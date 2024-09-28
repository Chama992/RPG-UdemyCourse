using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector2.zero;
    }

    public override void Update()
    {
        base.Update();
        if (player.IsGroundChecked())
            StateMachine.ChangeState(player.IdleState);
        if (player.IsWallChecked())
            StateMachine.ChangeState(player.WallSlideState);
        if (xInput != 0)
            player.SetVelocity(xInput * player.airMoveSpeedCoefficient, rb.velocity.y);
    }
}
