using UnityEngine;
using static PlayerHealth;

public class EnemyObjective : MonoBehaviour, ITargettable, IDamageable
{
    public Transform Target => transform;

    [SerializeField] private float stoppingDistance = 10;
    public float StoppingDistance { get { return stoppingDistance; } }

    [HideInInspector] public GameObject GameObject { get { return gameObject; } }

    public delegate void OnDamage(int Health);
    public event OnDamage onDamage;
    public int Health { get; set; } = 5000;

    public void OnHit(int damage)
    {       
        Health -= damage;
        onDamage?.Invoke(Health);
        if (Health <= 0)
            GameManager.Instance.OnLose();
    }
}
