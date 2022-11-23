using UnityEngine;

public class ShieldSpell : MonoBehaviour, ISpell
{
    private GameObject Shield;

    private void Start()
    {
        Shield = SpellManager.Instance.Shield;
        
    }

    public void Cast(Transform spawnPos, Animator animator)
    {
        SpawnShield();
        animator.SetTrigger("fireAttack");

    }
    public void SpawnShield()
    {
        Camera cam = SpellManager.Instance.cam;
        Instantiate(Shield, SpellManager.Instance.closeSpawnPos.position, new Quaternion(0, cam.transform.rotation.y, 
                                                                                            0, cam.transform.rotation.w));
    }
}
