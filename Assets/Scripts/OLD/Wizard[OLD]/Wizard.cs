using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour, IDamageable
{
    protected static float startSpeed = 3.0f;
    protected static float speed;
    protected Animator animator;
    protected CharacterController characterController;

    private bool isImmune = false;

    public int Health { get; protected set; }

    protected void Awake()
    {
        Health = 100;
        speed = startSpeed;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public Transform GetTarget()
    {
        return this.transform;
    }

    public void OnHit(int damage)
    {
        if (isImmune) { return; } else { isImmune = true; }

        StartCoroutine(ResolveImmunity());
        Health -= damage;

        animator.SetTrigger("Hit");

        if (Health <= 0)
            OnDeath();
    }

    private IEnumerator ResolveImmunity()
    {
        yield return new WaitForSeconds(1);
        isImmune = false;
    }

    public void OnDeath()
    {
        animator.SetTrigger("Die");
        speed = 0;
    }
}
