using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    EnemyStateManager enemy;
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("isWalking", true);
    }

    public override void UpdateState()
    {
        enemy.navMeshAgent.destination = enemy.CurrentTarget.transform.position;

        if (enemy.navMeshAgent.remainingDistance <= 0.5)
            enemy.SwitchState(enemy.AttackState);
    }
}
