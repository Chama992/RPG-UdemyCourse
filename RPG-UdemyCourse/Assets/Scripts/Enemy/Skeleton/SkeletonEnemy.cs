using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : Enemy
{
    #region
    public SkeletonEnemyIdleState IdleState { get; private set; }
    public SkeletonEnemyMoveState MoveState { get; private set; }
    public SkeletonEnemyBattleState BattleState { get; private set; }
    public SkeletonAttackState AttackState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        IdleState = new SkeletonEnemyIdleState(EnemyStateMachine, this, "Idle", this);
        MoveState = new SkeletonEnemyMoveState(EnemyStateMachine, this, "Move", this);
        BattleState = new SkeletonEnemyBattleState(EnemyStateMachine, this, "Move", this);
        AttackState = new SkeletonAttackState(EnemyStateMachine, this, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        EnemyStateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
