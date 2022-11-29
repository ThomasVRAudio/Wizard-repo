using System.Collections;
using UnityEngine;


public class EnemyAttackState : EnemyBaseState
{
    protected string anim = "smallAttack";
    protected EnemyStateManager enemy;

    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        anim = enemy.CurrentTarget.GameObject.name == "ObjectiveTargetCylinder" ? "bigAttack" : "smallAttack";
        enemy.animator.SetBool("isWalking", false);
        enemy.navMeshAgent.speed = 0;
        StartAnimation(enemy);
    }

    public override void UpdateState()
    {
        var targetRotation = Quaternion.LookRotation(enemy.CurrentTarget.GameObject.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 8 * Time.deltaTime);

        if (enemy.navMeshAgent.remainingDistance >= enemy.CurrentTarget.StoppingDistance + 5)
        {
            if (enemy.navMeshAgent.remainingDistance > float.MaxValue || enemy.navMeshAgent.pathPending)
                return;
            enemy.SwitchState(enemy.ChaseState);

        }
    }

    protected virtual void StartAnimation(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("isAnimatingAttack", true);
        enemy.SetColliders(true);

        enemy.animator.SetTrigger(anim);
        enemy.OnEndAnimationCallback += OnEndAnimation;
    }

    protected void OnEndAnimation()
    {
        enemy.animator.SetBool("isAnimatingAttack", false);
        enemy.SetColliders(false);
        enemy.OnEndAnimationCallback -= OnEndAnimation;
        StartCoroutine(RepeatAttack());

    }

    protected IEnumerator RepeatAttack()
    {
        yield return new WaitForSeconds(0.5f);
        if (enemy.CurrentState == enemy.AttackState) StartAnimation(enemy);
    }
}
