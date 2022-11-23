using UnityEngine;

public class ProjectileSpellObject : SpellCreation
{
    [SerializeField] private int damage = 50;
    protected override void SetDamage(Collider target)
    {
        target.gameObject.GetComponent<IDamageable>().OnHit(damage + baseDamage);
        Destroy(gameObject, 0.1f);
    }
}
