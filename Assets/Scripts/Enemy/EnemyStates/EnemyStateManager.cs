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

    public GameObject Objective { get; set; }
    public GameObject CurrentTarget { get; set; }

    public Animator animator => GetComponent<Animator>();
    public NavMeshAgent navMeshAgent;

    public DamageCollider[] damageColliders { get; set; }
    #endregion

    public delegate void OnEndAnimation();
    public event OnEndAnimation OnEndAnimationCallback;

   

    public void SetObjective(GameObject Objective)
    {
        
        this.Objective = Objective;
        CurrentTarget = Objective;
        CurrentState = WalkState;
        CurrentState.EnterState(this);
    }

    private void Awake()
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

    private void Update()
    {
        if (CurrentTarget != null)
            CurrentState.UpdateState();
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

        if(CurrentState != DeathState)
            SwitchState(WalkState);
    }

    public void EndAnimation()
    {
        navMeshAgent.speed = baseSpeed;
        OnEndAnimationCallback?.Invoke();
    }

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
}

