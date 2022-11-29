using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] int Damage = 10;

    private void Awake() => GetComponent<Collider>().enabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
            other.gameObject.GetComponent<IDamageable>().OnHit(Damage);
        Destroy(this.gameObject);
    }

}

