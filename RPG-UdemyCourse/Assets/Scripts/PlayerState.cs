using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine StateMachine;
    protected Player player;
    private string animBoolName;

    public PlayerState(Player _player, PlayerStateMachine _playerStateMachine, string _animBoolName)
    { 
        this.StateMachine = _playerStateMachine;
        this.player = _player;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.Anim.SetBool(animBoolName, true);
    }

    public virtual void Update()
    { 
    }

    public virtual void Exit() 
    {
        player.Anim.SetBool(animBoolName, false);
    }
}
