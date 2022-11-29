using UnityEngine;

public class TornadoSpell : MonoBehaviour, ISpell
{
    private GameObject TornadoVFX;
    private GameObject tornado;
    private Transform spawnPos;
    
    public void Start()
    {
        TornadoVFX = SpellManager.Instance.TornadoVFX;
        spawnPos = SpellManager.Instance.TornadoTarget;
    }

    public void Cast(Transform spawnPos, Animator animator) => animator.SetTrigger("tornadoAttack");

    public void SpawnTornado()
    {
            Vector3 spawningPosition = (Physics.Raycast(SpellManager.Instance.cam.transform.position, 
                                                        SpellManager.Instance.cam.transform.forward, 
                                                        out RaycastHit hit, 80, 
                                                        SpellManager.Instance.GroundLayer) // changed 12 to 80
            && hit.transform.gameObject.layer == 6) ? 
            hit.point : spawnPos.position;

        tornado = Instantiate(TornadoVFX, spawningPosition, Quaternion.identity);
        Destroy(tornado, 5.0f);

        CameraShake.Instance.ShakeCamera(3f, 3f);


    }  
}

