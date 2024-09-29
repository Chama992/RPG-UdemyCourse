using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
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
        if (!player.IsGroundChecked())
            StateMachine.ChangeState(player.AirState);
        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundChecked())
            StateMachine.ChangeState(player.JumpState);
        if (Input.GetKeyDown(KeyCode.Mouse0))
            StateMachine.ChangeState(player.PrimaryAttackState);
    }
}
