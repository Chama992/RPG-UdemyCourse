using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine enemyStateMachine;
    protected Enemy enemyBase;
    protected string animBoolName;
    protected bool animTriggerCalled;
    protected float stateTimer;
    public EnemyState(EnemyStateMachine _enemyStateMachine, Enemy _enemyBase, string _animBoolName)
    { 
        this.enemyStateMachine = _enemyStateMachine;
        this.enemyBase = _enemyBase;
        this.animBoolName = _animBoolName;
    }
    public virtual void Enter()
    {
        animTriggerCalled = false;
        enemyBase.Anim.SetBool(animBoolName, true);
    }
    public virtual void Exit() 
    {
        enemyBase.Anim.SetBool(animBoolName, false);
    }
    public virtual void Update() 
    {
        stateTimer -= Time.deltaTime;
    }
    public void AnimationFinishTrigger()
    {
        animTriggerCalled = true;
    }
}
