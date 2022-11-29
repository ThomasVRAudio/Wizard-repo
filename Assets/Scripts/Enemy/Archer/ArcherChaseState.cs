using UnityEngine;

public class ArcherChaseState : EnemyChaseState
{
    public override void UpdateState()
    {
        Vector3 pos = enemy.CurrentTarget.GameObject.transform.position - enemy.transform.position;
        if (Mathf.Sqrt(Vector3.Dot(pos, pos)) <= enemy.CurrentTarget.StoppingDistance + 100)
        {
            enemy.SwitchState(enemy.AttackState);
        }
    }
}
