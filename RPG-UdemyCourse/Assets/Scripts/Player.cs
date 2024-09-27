using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Components
    public Animator Anim { get; private set; }
    #endregion
    #region PlayerStates
    public PlayerStateMachine StateMachine { get; private set;  }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    #endregion
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, "Idle");
        MoveState = new PlayerMoveState(this, StateMachine, "Move");
    }

    private void Start()
    {
        Anim = GetComponentInChildren<Animator>();
        StateMachine.Initialize(IdleState);
    }
    private void Update()
    {
        StateMachine.currentState.Update();
    }

}
