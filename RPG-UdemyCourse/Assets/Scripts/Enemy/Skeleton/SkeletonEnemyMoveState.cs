using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyMoveState : SkeletonEnemyGroundState
{
    public SkeletonEnemyMoveState(EnemyStateMachine _enemyStateMachine, Enemy _enemyBase, string _animBoolName, SkeletonEnemy _enemy) : base(_enemyStateMachine, _enemyBase, _animBoolName, _enemy)
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
        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, enemy.Rb.velocity.y);
        if (enemy.IsWallChecked() || !enemy.IsGroundChecked())
        {
            enemyStateMachine.ChangeState(enemy.IdleState);
            enemy.Flip();
        }     
    }
}
