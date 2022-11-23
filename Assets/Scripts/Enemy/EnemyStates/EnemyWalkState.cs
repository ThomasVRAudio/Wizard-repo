using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalkState : EnemyBaseState
{
    EnemyStateManager enemy;
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.navMeshAgent.destination = enemy.CurrentTarget.transform.position;
        enemy.animator.SetBool("isWalking", true);
    }


    public override void UpdateState()
    {
        Vector3 pos = enemy.Objective.transform.position - enemy.transform.position;
        if (Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y + pos.z * pos.z) <= 0.5f)
            enemy.SwitchState(enemy.AttackState);
    }
}
