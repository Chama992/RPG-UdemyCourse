using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine EnemyStateMachine { get; private set; }
    [HideInInspector]public Transform player;
    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    [Header("Attack Info")]
    public LayerMask whatIsPlayer;
    public float playerDetectDistance;
    public float attackDistance;
    public float battleMoveSpeed;
    public float attackCoolDown;
    public float battleTime;
    public float quitBattleDistance;
    [HideInInspector]public float lastTimeAttack;
    protected override void Awake()
    {
        base.Awake();
        EnemyStateMachine = new EnemyStateMachine();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        EnemyStateMachine.currentState.Update();
    }
    public RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(transform.position, facingDir * Vector2.right, playerDetectDistance, whatIsPlayer );
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x + playerDetectDistance * facingDir,transform.position.y));
    }
    /// <summary>
    /// use for check the currentstate anim finished?
    /// </summary>
    public void AnimationTrigger() => this.EnemyStateMachine.currentState.AnimationFinishTrigger();
    public bool CanAttack()
    {
        if (lastTimeAttack + attackCoolDown < Time.time)
            return true;
        else
            return false;
    }

    public override void GetDamage()
    {
        base.GetDamage();
    }
}