using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (xInput != 0 && !player.IsBusy)
        {
            // near wall cant move
            if (player.IsWallChecked())
            {
                if (xInput != player.facingDir)
                    StateMachine.ChangeState(player.MoveState);
            }
            else
                StateMachine.ChangeState(player.MoveState);
        }
        
    }
}
