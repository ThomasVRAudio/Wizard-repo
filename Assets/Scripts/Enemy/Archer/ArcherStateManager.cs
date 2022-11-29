using UnityEngine.AI;
using UnityEngine;

public class ArcherStateManager : EnemyStateManager
{

    protected override void Beginning()
    {
        #region Add States
        AttackState = gameObject.AddComponent<ArcherAttackState>();
        ChaseState = gameObject.AddComponent<ArcherChaseState>();
        WalkState = gameObject.AddComponent<ArcherWalkState>();
        DeathState = gameObject.AddComponent<EnemyDeathState>();
        HitState = gameObject.AddComponent<EnemyHitState>();
        #endregion
        navMeshAgent = GetComponent<NavMeshAgent>();
        damageColliders = GetComponentsInChildren<DamageCollider>();
        SetColliders(false);
    }

    public override void SetStoppingDistance(ITargettable target)
    {
        navMeshAgent.stoppingDistance = target.StoppingDistance + 100;
    }
}

