using UnityEngine;


public class PickupItem : MonoBehaviour
{
    private float rotationSpeed = 70f;
    private float bounceSpeed = 0.2f;
    [SerializeField] protected AnimationCurve curve;
    private Animator animator;
    private float time = 0f;
    private Vector3 startPosition;
    public enum PickupType { speed, damage, skill }
    public PickupType itemType;

    private void Awake()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        IdleAnim();
    }

    private void IdleAnim()
    {
        transform.Rotate(0, 0, 1f * rotationSpeed * Time.deltaTime);

        if (time < 1)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, startPosition + new Vector3(0, 0.1f, 0), curve.Evaluate(time));
            time += Time.deltaTime * bounceSpeed;
        }
        else
        {
            time = 0f;
        }
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
