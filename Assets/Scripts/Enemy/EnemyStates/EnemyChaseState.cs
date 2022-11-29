
using UnityEngine;
using UnityEngine.AI;

public class EnemyChaseState : EnemyBaseState
{
    protected EnemyStateManager enemy;
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("isWalking", true);
        enemy.navMeshAgent.speed = enemy.baseSpeed;

    }

    public override void UpdateState()
    {
        if (enemy.navMeshAgent.remainingDistance <= enemy.CurrentTarget.StoppingDistance && !enemy.navMeshAgent.pathPending)
            enemy.SwitchState(enemy.AttackState);
    }
}

