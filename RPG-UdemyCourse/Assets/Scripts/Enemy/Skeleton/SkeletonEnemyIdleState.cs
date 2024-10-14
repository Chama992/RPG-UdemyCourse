using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyIdleState : SkeletonEnemyGroundState
{
    public SkeletonEnemyIdleState(EnemyStateMachine _enemyStateMachine, Enemy _enemyBase, string _animBoolName, SkeletonEnemy _enemy) : base(_enemyStateMachine, _enemyBase, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer <= 0)
            enemyStateMachine.ChangeState(enemy.MoveState);
    }
}
