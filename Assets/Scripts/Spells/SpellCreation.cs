using UnityEngine;

public abstract class SpellCreation : MonoBehaviour
{
    protected Collider target;
    public static int baseDamage;

    private void Awake()
    {
        baseDamage = PlayerStats.Instance.BaseDamage;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>() == null || other.gameObject.GetComponent<ITargettable>() != null) return;
        target = other;
        SetDamage(target);
    }

    protected abstract void SetDamage(Collider target);
}
