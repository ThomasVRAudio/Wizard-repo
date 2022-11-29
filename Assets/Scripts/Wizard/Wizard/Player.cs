using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour, ITargettable
{
    PlayerController controller;
    PlayerCombat combat;
    PlayerHealth playerHealth;
    public Animator animator;

    [SerializeField] private float stoppingDistance = 10;
    public float StoppingDistance { get { return stoppingDistance; } }

    [HideInInspector] public GameObject GameObject { get { return gameObject; } }

    public Transform cam;
    public float startSpeed = 1f;
    public float speed = 1f;

    public Transform Target => transform;

    public delegate void OnEndAnimation();
    public event OnEndAnimation OnEndAnimationCallback;

    private void Awake()
    {
        
        controller = GetComponent<PlayerController>();
        combat = GetComponent<PlayerCombat>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start()
    {
        startSpeed = PlayerStats.Instance.BaseSpeed;
        speed = startSpeed;
    }

    private void Update()
    {
        controller.MovementUpdate(this);
        combat.CombatUpdate(this);
    }

    public void EndAnimation() => OnEndAnimationCallback?.Invoke();

    public void UpdateSpeed(float x)
    {
        startSpeed = x;
        if (!animator.GetBool("IsAttacking")) speed = startSpeed;

    }

    public void Pickup(PickupItem item) => PlayerStats.Instance.Pickup(item);

    public void Heal(int health) => playerHealth.Heal(health);

}
