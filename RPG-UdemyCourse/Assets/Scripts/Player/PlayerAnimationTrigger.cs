using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    Player player => GetComponentInParent<Player>();

    public void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    public void AttackTrigger()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.AttackCheckRadius);
        if (collider2Ds.Length != 0)
        {
            foreach (var collider in collider2Ds)
            {
                if (collider.GetComponent<Enemy>())
                {
                    collider.GetComponent<Enemy>().GetDamage();
                }
            }
        }
    }
}
