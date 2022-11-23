using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public int Damage = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IDamageable>() == null) return;

            other.gameObject.GetComponent<IDamageable>().OnHit(Damage);
    }
}
