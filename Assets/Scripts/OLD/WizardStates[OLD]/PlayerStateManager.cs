using UnityEngine;

public class PlayerStateManager : MonoBehaviour, ITargettable, IDamageable
{
    #region States
    public PlayerBaseState CurrentState { get; set; }
    public PlayerMoveState MoveState { get; set; }
    public PlayerAttackState AttackState { get; set; }
    public PlayerDeathState DeathState { get; set; }
    public PlayerHitState HitState { get; set; }
    #endregion

    [SerializeField]
    private float startSpeed = 1.0f;
    [SerializeField]
    private int health = 100;

    public Transform cam;

    #region Properties
    public float StartSpeed 
    { 
        get { return startSpeed; } 
        set { startSpeed = value; } 
    }
    public float Speed { get; set; }
    public Animator animator { get; set; }
    public CharacterController CharacterController { get; protected set; }
    public Transform spellSpawnPos;
    public bool isDead { get; set; } = false;

    public int Health 
    { 
        get { return health; } 
        set { health = value; } 
    }
    public int Damage { get; set; }
    public bool IsImmune { get; set; } = false;
    public bool HasCast { get; set; } = false;

    public Spells SpellType { get; set; }
    #endregion

    public enum Spells { Fire, Earth, Tornado, Air }
    public Transform Target => transform;

    [SerializeField] private float stoppingDistance = 10;
    [HideInInspector] public float StoppingDistance { get { return stoppingDistance;  } }
    [HideInInspector] public GameObject GameObject => GameObject;

    public delegate void OnEndAnimation();
    public event OnEndAnimation OnEndAnimationCallback;

    void Awake()
    {
        animator = GetComponent<Animator>();
        CharacterController = GetComponent<CharacterController>();

        #region Add States
        MoveState = gameObject.AddComponent<PlayerMoveState>();
        AttackState = gameObject.AddComponent<PlayerAttackState>();
        DeathState = gameObject.AddComponent<PlayerDeathState>();
        HitState = gameObject.AddComponent<PlayerHitState>();
        #endregion

        CurrentState = MoveState;
        CurrentState.EnterState(this);
    }

    private void Start()
    {
        Speed = PlayerStats.Instance.BaseSpeed;
    }
    void Update()
    {
        if (CurrentState != null)
            CurrentState.UpdateState();


        if (HasCast) return;
        if (Input.GetKeyDown(KeyCode.Q)) SetSpell(Spells.Fire);
        if (Input.GetKeyDown(KeyCode.E)) SetSpell(Spells.Earth);
        if (Input.GetKeyDown(KeyCode.R)) SetSpell(Spells.Tornado);
        if (Input.GetKeyDown(KeyCode.T)) SetSpell(Spells.Air);

    }

    private void SetSpell(Spells spell)
    {
        if (CurrentState != MoveState) return;
        SpellType = spell;
        SwitchState(AttackState);
    }

    public void SwitchState(PlayerBaseState state)
    {
        CurrentState = state;
        state.EnterState(this);
    }

    public void EndAnimation()
    {
        Speed = StartSpeed;
        OnEndAnimationCallback?.Invoke();
    }



    public void OnHit(int damage)
    {
        Damage = damage;
        SwitchState(HitState);

        if (IsImmune) { return; } else { IsImmune = true; }

        Health -= Damage;
        Speed = 0;

        if (Health <= 0)
        {
            SwitchState(DeathState);
            return;
        }

        animator.SetBool("isRecovering", true);
        animator.SetTrigger("Hit");
        OnEndAnimationCallback += OnEndHitAnimation;
    }

    private void OnEndHitAnimation()
    {
        animator.SetBool("isRecovering", false);
        SwitchState(MoveState);
        IsImmune = false;
        OnEndAnimationCallback -= OnEndHitAnimation;
    }

    public void OnDeath() => animator.SetTrigger("Die");

    public void UpdateSpeed(float x)
    {
        StartSpeed = x;
        if (CurrentState == MoveState)
            Speed = StartSpeed;
    }
}
