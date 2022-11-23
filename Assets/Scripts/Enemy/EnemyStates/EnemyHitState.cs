using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyHitState : EnemyBaseState
{
    EnemyStateManager enemy;
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.Health -= enemy.Damage;

        if (enemy.Health <= 0)
        {
            enemy.SwitchState(enemy.DeathState);
            return;
        }
        
        if (enemy.Damage > enemy.IgnoreDamageAmount)
        {
            enemy.animator.SetTrigger("Hit");
            enemy.animator.SetBool("IsRecovering", true);
            enemy.OnEndAnimationCallback += OnEndAnimation;
            enemy.navMeshAgent.speed = 0;
        }
        enemy.SwitchState(enemy.PreviousState);
    }

    public override void UpdateState() { }

    private void OnEndAnimation()
    {
        enemy.animator.SetBool("IsRecovering", false);
        enemy.OnEndAnimationCallback -= OnEndAnimation;
    }
}
