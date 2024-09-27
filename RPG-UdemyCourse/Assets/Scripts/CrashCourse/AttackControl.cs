using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{   
    private PlayerOld player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerOld>();
    }

    // Update is called once per frame
    private void AnimationTrigger()
    { 
        player.QuitAttack();
    }
}
