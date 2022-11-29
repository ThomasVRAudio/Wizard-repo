using UnityEngine;

public class BowScript : MonoBehaviour
{
    [SerializeField] private GameObject bow;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform arrowNock;
    private GameObject arrowInstance;
    private ArcherStateManager stateManager;

    public void BowDraw()
    {
        bow.GetComponent<Animator>().SetTrigger("draw");
        arrowInstance = Instantiate(arrow, arrowNock.position, arrowNock.transform.rotation);
        arrowInstance.transform.parent = arrowNock;
        
    }

    public void BowShoot()
    {
        bow.GetComponent<Animator>().SetTrigger("shoot");
    }

    public void ArrowFire()
    {
        if (arrowInstance == null)
            return;

        stateManager = GetComponent<ArcherStateManager>();

        var targetPos = stateManager.CurrentTarget.GameObject.transform.position + Vector3.up * 2;
        arrowInstance.transform.parent = null;
        arrowInstance.GetComponent<Collider>().enabled = true;

        var arrowPos = arrowInstance.transform.position;
        arrowInstance.GetComponent<Rigidbody>().AddForce((targetPos - arrowPos).normalized * 400);

        if(arrowInstance != null)
            Destroy(arrowInstance, 5f);
    }


}
