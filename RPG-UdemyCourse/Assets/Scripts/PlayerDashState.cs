using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Exit()
    {
        // return the velocity
        player.SetVelocity(0, rb.velocity.y);
        //rb.velocity = new Vector2(0, rb.velocity.y);
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        //while dashing, y velocity dont change
        player.SetVelocity(player.dashSpeed * player.dashDir, 0);
        //rb.velocity = new Vector2(player.dashSpeed * player.dashDir, 0);
        if (!player.IsGroundChecked() && player.IsWallChecked())
            StateMachine.ChangeState(player.WallSlideState);
        if (stateTimer < 0)
            StateMachine.ChangeState(player.IdleState);
    }
}
