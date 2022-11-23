using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    Player player;
    private bool IsImmune = false;
    public int Health { get; set; } = 100;
    public int Damage { get; set; } = 0;

    private void Awake() => player = GetComponent<Player>();
    public void OnHit(int damage)
    {
        Damage = damage;

        if (IsImmune) { return; } else { IsImmune = true; }

        Health -= Damage;
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

    private void OnEndAnimation()
    {
        player.speed = player.startSpeed;
        player.animator.SetBool("isRecovering", false);
        IsImmune = false;
        player.OnEndAnimationCallback -= OnEndAnimation;
    }
}
