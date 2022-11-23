using System;
using UnityEngine;

public class AirSpell : MonoBehaviour, ISpell
{
    private GameObject Airsphere;

    private void Start()
    {
        Airsphere = SpellManager.Instance.Airsphere;
    }

    public void Cast(Transform spawnPos, Animator animator) => animator.SetTrigger("airAttack");
    public void SpawnAir() => Instantiate(Airsphere, transform.position, Quaternion.identity);
}
