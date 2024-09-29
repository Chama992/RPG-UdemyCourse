using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int ComboCounter;
    private float ComboWindow = 2;
    private float lastTimeAttacked; 

    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName) : base(_player, _playerStateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (ComboCounter > 2 || Time.time - lastTimeAttacked > ComboWindow)
            ComboCounter = 0;
        player.Anim.SetInteger("ComboCounter", ComboCounter);
        float attackDir = player.facingDir;
        if (xInput != 0)
            attackDir = xInput;
        player.SetVelocity(player.attackMove[ComboCounter].x * attackDir, player.attackMove[ComboCounter].y);
        stateTimer = .1f;// move a little
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
        ComboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            player.ZeroVelocity();
        if (aniTriggerCalled)
            StateMachine.ChangeState(player.IdleState);
    }
}
