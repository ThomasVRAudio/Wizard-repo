using System.Collections;
using UnityEngine;


public class EnemyAttackState : EnemyBaseState
{
    string anim = "smallAttack";
    EnemyStateManager enemy;

    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        anim = enemy.CurrentTarget.name == "ObjectiveTarget" ? "bigAttack" : "smallAttack";
        enemy.animator.SetBool("isWalking", false);
        enemy.navMeshAgent.speed = 0;      

        StartAnimation(enemy);
    }

    public override void UpdateState()
    {
        Vector3 targetPosition = enemy.CurrentTarget.transform.position;
        enemy.navMeshAgent.destination = targetPosition;

        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 8 * Time.deltaTime);

        if (enemy.navMeshAgent.remainingDistance >= 0.7)
            enemy.SwitchState(enemy.ChaseState);
    }

    void StartAnimation(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("isAnimatingAttack", true);
        enemy.SetColliders(true);

        enemy.animator.SetTrigger(anim);
        enemy.OnEndAnimationCallback += OnEndAnimation;
    }

    void OnEndAnimation()
    {
        enemy.animator.SetBool("isAnimatingAttack", false);
        enemy.SetColliders(false);
        enemy.OnEndAnimationCallback -= OnEndAnimation;
        StartCoroutine(RepeatAttack());

    }

    private IEnumerator RepeatAttack()
    {
        yield return new WaitForSeconds(0.5f);
        if (enemy.CurrentState == enemy.AttackState) StartAnimation(enemy);
    }
}
