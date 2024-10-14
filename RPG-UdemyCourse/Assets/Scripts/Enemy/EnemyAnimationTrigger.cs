using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    Enemy enemy => GetComponentInParent<Enemy>();
    public void AnimationTrigger()
    {
        enemy.AnimationTrigger();
    }
    public void AttackTrigger()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(enemy.AttackCheck.position, enemy.AttackCheckRadius);
        if (collider2Ds.Length != 0)
        {
            foreach (var collider in collider2Ds)
            {
                if (collider.GetComponent<Player>())
                {
                    collider.GetComponent<Player>().GetDamage();
                }
            }
        }
    }
}
