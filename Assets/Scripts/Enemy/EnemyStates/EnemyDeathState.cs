using System.Collections;
using UnityEngine;

public class EnemyDeathState : EnemyBaseState
{

    public override void EnterState(EnemyStateManager enemy)
    {
        enemy.animator.SetTrigger("Die");
        enemy.navMeshAgent.speed = 0;
        StartCoroutine(RemoveObject(enemy));
        enemy.SetColliders(false);
        enemy.gameObject.GetComponent<Collider>().enabled = false;
        SpawnManager.Instance.RemoveFromList(this.gameObject);
        
        DropChance.Instance.Chance(this.gameObject);
    }


    public override void UpdateState() { }


    private IEnumerator RemoveObject(EnemyStateManager enemy)
    {
        yield return new WaitForSeconds(6f);
        Destroy(enemy.gameObject);
    }
}
