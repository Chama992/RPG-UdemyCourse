using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyBattleState : SkeletonEnemyState
{
    private int battleFacing;

    public SkeletonEnemyBattleState(EnemyStateMachine _enemyStateMachine, Enemy _enemyBase, string _animBoolName, SkeletonEnemy _enemy) : base(_enemyStateMachine, _enemyBase, _animBoolName, _enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = enemy.battleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance && enemy.CanAttack())
            {
                enemyStateMachine.ChangeState(enemy.AttackState);
                return;
            }
        }
        else
        {
            if (stateTimer <= 0 || Vector2.Distance(enemy.transform.position, enemy.player.position) > enemy.quitBattleDistance)
                enemyStateMachine.ChangeState(enemy.IdleState);
        }
        if (enemy.player.position.x > enemy.transform.position.x)
            battleFacing = 1;
        else if (enemy.player.position.x < enemy.transform.position.x)
            battleFacing = -1;
        enemy.SetVelocity(enemy.battleMoveSpeed * battleFacing, enemy.Rb.velocity.y);
    }
}
