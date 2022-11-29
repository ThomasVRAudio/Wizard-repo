using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpell : MonoBehaviour, ISpell
{
    private GameObject Earthball;
    private Transform spawnPos;


    private void Start()
    {
        Earthball = SpellManager.Instance.Earthball;
    }

    public void Cast(Transform spawnPos, Animator animator)
    {
        this.spawnPos = spawnPos;

        animator.SetTrigger("earthAttack");
        StartCoroutine(SpawnEarth(0f));
    }

    private IEnumerator SpawnEarth(float interval)
    {
        yield return new WaitForSeconds(interval);
        GameObject spawnedRock = Instantiate(Earthball, spawnPos.position + (new Vector3(1,1,0) * 50f), 
            Quaternion.identity);
        Destroy(spawnedRock, 12.0f);
    }
}
