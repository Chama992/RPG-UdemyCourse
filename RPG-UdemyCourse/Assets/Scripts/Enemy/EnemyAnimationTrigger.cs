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
}
