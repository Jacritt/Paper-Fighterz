using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laminator_BC : BaseCharacter
{
    
    public override void DownAttack()
    {
        Debug.Log("Performing Down Attack");
        ChangeAnimationState("DOWN_ATTACK");
        isAttacking = true;
        // Set the attack cooldown based on the attack animation duration
        attackTimer = GetAttackAnimationDuration();
        gameObject.transform.position = new Vector2(6.5f, transform.position.y);
        print("Teleport");
    }
}
