using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemyState : EnemyState
{
    protected SkeletonEnemy enemy;
    public SkeletonEnemyState(EnemyStateMachine _enemyStateMachine, Enemy _enemyBase, string _animBoolName, SkeletonEnemy _enemy) : base(_enemyStateMachine, _enemyBase, _animBoolName)
    {
        enemy = _enemy;
    }
}
