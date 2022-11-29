using UnityEngine;
using UnityEngine.AI;


public class EnemyStateManager : MonoBehaviour, IDamageable
{
    #region States
    public EnemyBaseState CurrentState { get; set; }
    public EnemyBaseState PreviousState { get; set; }
    public EnemyAttackState AttackState { get; set; }
    public EnemyChaseState ChaseState { get; set; }
    public EnemyWalkState WalkState { get; set; }
    public EnemyDeathState DeathState { get; set; }
    public EnemyHitState HitState { get; set; }

    #endregion

    public float baseSpeed = 1;
    
    [SerializeField]
    private int health = 200;

    [SerializeField]
    private int ignoreDamageCount = 15;

    #region Properties
    public int Health 
    { 
        get { return health; } 
        set { health = value; } 
    }

    public int IgnoreDamageAmount
    {
        get { return ignoreDamageCount; }
        set { ignoreDamageCount = value; }
    }
    public int Damage { get; set; } = 0;

    public ITargettable Objective { get; set; }
    public ITargettable CurrentTarget { get; set; }

    public Animator animator => GetComponent<Animator>();
    public NavMeshAgent navMeshAgent;

    public DamageCollider[] damageColliders { get; set; }
    #endregion

    public delegate void OnEndAnimation();
    public event OnEndAnimation OnEndAnimationCallback;

    private Vector3 targetPosition;
    public void SetObjective(ITargettable target)
    {
        
        this.Objective = target;
        CurrentTarget = Objective;
        SetStoppingDistance(target);
        CurrentState = WalkState;
        CurrentState.EnterState(this);
    }

    public virtual void SetStoppingDistance(ITargettable target) => navMeshAgent.stoppingDistance = target.StoppingDistance;

    private void Awake() => Beginning();

    protected virtual void Beginning()
    {
        #region Add States
        AttackState = gameObject.AddComponent<EnemyAttackState>();
        ChaseState = gameObject.AddComponent<EnemyChaseState>();
        WalkState = gameObject.AddComponent<EnemyWalkState>();
        DeathState = gameObject.AddComponent<EnemyDeathState>();
        HitState = gameObject.AddComponent<EnemyHitState>();
        #endregion
        navMeshAgent = GetComponent<NavMeshAgent>();
        damageColliders = GetComponentsInChildren<DamageCollider>();
        SetColliders(false);
    }

    private void Start() => targetPosition = CurrentTarget.GameObject.transform.position;

    private void Update()
    {
        if (CurrentTarget != null)
            CurrentState.UpdateState();

        Vector3 currentPositionTarget = CurrentTarget.GameObject.transform.position;
        if (targetPosition != currentPositionTarget)
        {
            navMeshAgent.destination = currentPositionTarget;
            targetPosition = currentPositionTarget;
        }
    }

    public void SwitchState(EnemyBaseState state)
    {
        if (CurrentState == DeathState)
            return;

        PreviousState = CurrentState;
        CurrentState = state;
        state.EnterState(this);
    }

    public void ResetTarget()
    {
        CurrentTarget = Objective;
        SetStoppingDistance(Objective);

        if(CurrentState != DeathState)
            SwitchState(WalkState);
    }

    public void EndAnimation() => OnEndAnimationCallback?.Invoke();

    public void OnHit(int damage)
    {
        if (CurrentState == DeathState) return;
        Damage = damage;  
        SwitchState(HitState);
    }


    public void SetColliders(bool active)
    {
        foreach (DamageCollider dCollider in damageColliders)
        {
            dCollider.gameObject.SetActive(active);
        }
    }

    public void SetTarget(ITargettable target)
    {
        CurrentTarget = target;
        SetStoppingDistance(target);
    }
}

