using UnityEngine;
public class SpellManager : MonoBehaviour
{
    public GameObject Airsphere;
    public GameObject Earthball;
    public GameObject TornadoVFX;
    public GameObject Fireball;
    public Transform spawnPos;
    public Transform closeSpawnPos;
    public Transform TornadoTarget;
    public LayerMask GroundLayer;
    public LayerMask ProjectileLayer;
    public Camera cam;
    public GameObject Shield;
    [SerializeField] float range = 350f;
    public static SpellManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        } else
        {
            Instance = this;
        }
    }
    public Vector3 ShootDirection(LayerMask layerMask)
    {
        Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, range, layerMask);
            return hit.collider == null ? cam.transform.forward * 30 : hit.point; // changed 8 to 30     
    }
}
