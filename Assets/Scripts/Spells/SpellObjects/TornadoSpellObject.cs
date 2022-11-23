using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoSpellObject : SpellCreation
{
    [SerializeField] private int damage = 50;
    protected override void SetDamage(Collider target)
    {
        this.target = target;
        StartCoroutine(DealDamage());
    }

    private IEnumerator DealDamage()
    {
        if (target == null) yield break;
        target.gameObject.GetComponent<IDamageable>().OnHit(damage + baseDamage);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DealDamage());
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }
}
