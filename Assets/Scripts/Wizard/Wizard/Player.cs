using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour, ITargettable
{
    PlayerController controller;
    PlayerCombat combat;
    PlayerHealth playerHealth;
    public Animator animator;

    public Transform cam;
    public float startSpeed = 1f;
    public float speed = 1f;

    public Transform Target => transform;

    public delegate void OnEndAnimation();
    public event OnEndAnimation OnEndAnimationCallback;

    private void Awake()
    {
        speed = startSpeed;
        controller = GetComponent<PlayerController>();
        combat = GetComponent<PlayerCombat>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Start() => startSpeed = PlayerStats.Instance.BaseSpeed;

    private void Update()
    {
        controller.MovementUpdate(this);
        combat.CombatUpdate(this);
    }

    public void EndAnimation() => OnEndAnimationCallback?.Invoke();
    //public void OnHit(int damage) => playerHealth.OnHit(damage);

    public void UpdateSpeed(float x)
    {
        startSpeed = x;
        if (!animator.GetBool("IsAttacking")) speed = startSpeed;

    } 

}
