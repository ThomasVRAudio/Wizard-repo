using UnityEngine;

public class ArcherAttackState : EnemyAttackState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        this.enemy = enemy;
        enemy.animator.SetBool("isWalking", false);
        enemy.navMeshAgent.speed = 0;      

        StartAnimation(enemy);
    }

    public override void UpdateState()
    {
        Vector3 targetPosition = enemy.CurrentTarget.GameObject.transform.position;

        var targetRotation = Quaternion.LookRotation(targetPosition - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 8 * Time.deltaTime);

        Vector3 pos = enemy.CurrentTarget.GameObject.transform.position - enemy.transform.position;
        if (Mathf.Sqrt(Vector3.Dot(pos, pos)) >= enemy.CurrentTarget.StoppingDistance + 130)
            enemy.SwitchState(enemy.ChaseState);
    }

    protected override void StartAnimation(EnemyStateManager enemy)
    {
        enemy.animator.SetBool("isAnimatingAttack", true);
        enemy.animator.SetTrigger("draw");
        enemy.OnEndAnimationCallback += Shoot;
    }

    public void Shoot()
    {
        enemy.OnEndAnimationCallback -= Shoot;
        enemy.OnEndAnimationCallback += OnEndAnimation;
        enemy.animator.SetTrigger("shoot");
    }
}
