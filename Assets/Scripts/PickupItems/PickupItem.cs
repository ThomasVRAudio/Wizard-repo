using UnityEngine;


public class PickupItem : MonoBehaviour
{
    private float rotationSpeed = 70f;
    private float bounceSpeed = 0.2f;
    [SerializeField] protected AnimationCurve curve;
    [SerializeField] private AnimationCurve bounceCurve;
    private Animator animator;
    private float time = 0f;
    private Vector3 startPosition;
    public enum PickupType { speed, damage, skill, health }
    public PickupType itemType;

    private delegate void Animated();
    private Animated animate;
    private Vector3 randomDir;

    public GameObject godRay;
    private GameObject godRayInstance;


    private void Awake()
    {
        startPosition = transform.parent.transform.position;
        animator = GetComponent<Animator>();
        animate = IntroBounce;
        randomDir = new Vector3(Random.Range(-1, 1), 0, Random.Range(-1,1));
    }

    private void Update() => animate();

    private void IntroBounce()
    {
        if (time < 2)
        {
            float lerpY = Mathf.Lerp(startPosition.y, startPosition.y + 10, bounceCurve.Evaluate(time));
            float lerpDirx = Mathf.Lerp(startPosition.x, startPosition.x + randomDir.x * 15, time);
            float lerpDirz = Mathf.Lerp(startPosition.z, startPosition.z + randomDir.z * 15, time);
            transform.parent.transform.position = new Vector3(lerpDirx, lerpY, lerpDirz);

            time += Time.deltaTime * bounceSpeed * 3;
        } else
        {
            time = 0f;
            startPosition = transform.parent.transform.position;
            animate = IdleAnim;
            godRayInstance = Instantiate(godRay, transform.position, godRay.transform.rotation);
        }
    }

    private void IdleAnim()
    {
        transform.Rotate(0, 1f * rotationSpeed * Time.deltaTime, 0);

        if (time < 1)
        {
            gameObject.transform.parent.transform.position = Vector3.Lerp(startPosition, startPosition + new Vector3(0, 2f, 0), curve.Evaluate(time));
            time += Time.deltaTime * bounceSpeed;
        }
        else
        {
            time = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() == null) return;

        if(godRayInstance != null)
            godRayInstance.GetComponent<GodRay>().OnStop();

        animator.SetTrigger("PickUp");
        other.GetComponent<Player>().Pickup(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerStateManager>() == null) return;
        animator.SetBool("isPickedUp", true);
    }

    public virtual void OnEndAnimation()
    {
        Destroy(transform.parent.gameObject);
    }
}
