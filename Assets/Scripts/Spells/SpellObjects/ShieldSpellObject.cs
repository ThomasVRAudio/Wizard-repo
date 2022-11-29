using UnityEngine;

public class ShieldSpellObject : SpellCreation
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] float speed = 0.2f;
    private float time = 0;
    private Vector3 originPos;
    private Vector3 targetPos;

    private void Awake()
    {
        originPos = transform.position;
        targetPos = SpellManager.Instance.ShootDirection(SpellManager.Instance.GroundLayer);
        float distance = Mathf.Sqrt(Vector3.Dot(targetPos - originPos, targetPos - originPos));
        targetPos = distance > 50 ? originPos + transform.forward * 50 : targetPos; // changed 3 to 20

    }

    void Update()
    {
        if (time < 1)
        {
            gameObject.transform.position = Vector3.Lerp(originPos, targetPos, curve.Evaluate(time));
            time += Time.deltaTime * speed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void SetDamage(Collider target)
    {
        target.gameObject.GetComponent<IDamageable>().OnHit(0);
    }
}
