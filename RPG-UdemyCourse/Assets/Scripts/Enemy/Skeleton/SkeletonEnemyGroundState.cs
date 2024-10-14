using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyGroundState : SkeletonEnemyState
{
    public SkeletonEnemyGroundState(EnemyStateMachine _enemyStateMachine, Enemy _enemyBase, string _animBoolName, SkeletonEnemy _enemy) : base(_enemyStateMachine, _enemyBase, _animBoolName, _enemy)
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
        if (enemy.IsPlayerDetected())
            enemyStateMachine.ChangeState(enemy.BattleState);
    }
}
