using System.Collections;
using UnityEngine;

public class FireSpell : MonoBehaviour, ISpell
{
    private GameObject Fireball;
    private Transform spawnPos;
    private Animator animator;
    

    public void Start()
    {
        Fireball = SpellManager.Instance.Fireball;
    }
    public void Cast(Transform spawnPos, Animator animator)
    {
        this.spawnPos = spawnPos;
        this.animator = animator;
        animator.SetTrigger("fireAttack");
        StartCoroutine(LoopCast());
        
    }

    public void SpawnFire()
    {
        GameObject spawnedRock = Instantiate(Fireball, spawnPos.position, Quaternion.identity);

        //spawnedRock.GetComponent<Rigidbody>().AddRelativeForce((
         //   SpellManager.Instance.ShootDirection(SpellManager.Instance.ProjectileLayer) - spawnedRock.transform.position).normalized * 200f);


        Destroy(spawnedRock, 2.0f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            StopAllCoroutines();
    }

    private IEnumerator LoopCast()
    {
        StartCoroutine(SpawnIntervallic(0.5f, 3));
        animator.SetTrigger("fireAttack");
        yield return new WaitForSeconds(1);

        StartCoroutine(LoopCast());
    }

    private IEnumerator SpawnIntervallic(float interval, int repeat)
    {

        yield return new WaitForSeconds(interval);
        if (repeat <= 0)
            yield break;

        StartCoroutine(SpawnIntervallic(0.1f, repeat -= 1));
        SpawnFire();
    }
}
