using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    Player player;
    private bool IsImmune = false;
    public int Health { get; set; } = 100;
    public int Damage { get; set; } = 0;

    public delegate void OnDamage(int health);
    public event OnDamage onDamage;

    private void Awake() => player = GetComponent<Player>();
    public void OnHit(int damage)
    {
        Damage = damage;

        if (IsImmune) { return; } else { IsImmune = true; }

        Health -= Damage;
        onDamage?.Invoke(Health);
        player.speed = 0;

        if (Health <= 0)
        {
            OnDeath();
            return;
        }

        player.animator.SetBool("isRecovering", true);
        player.animator.SetTrigger("Hit");
        player.OnEndAnimationCallback += OnEndAnimation;
    }

    public void OnDeath() => player.animator.SetTrigger("Die");

    public void Heal(int health)
    {
        Health = Mathf.Clamp(Health += health, 0, PlayerStats.Instance.BaseHealth);
        HealthUI.Instance.SetHealthBar(Health);
    }

    private void OnEndAnimation()
    {
        player.speed = player.startSpeed;
        player.animator.SetBool("isRecovering", false);
        IsImmune = false;
        player.OnEndAnimationCallback -= OnEndAnimation;
    }

}
