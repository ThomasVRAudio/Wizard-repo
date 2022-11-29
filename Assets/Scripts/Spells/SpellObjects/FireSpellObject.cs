using UnityEngine;

public class FireSpellObject : ProjectileSpellObject
{
    private float time = 0;
    private Vector3 originPos;
    private Vector3 targetPos;
    private float magnitude = 0;
    private const float speed = 100f; //used to be 5f
    private static int fireball = 0;
    private Vector3 projectileOffset = Vector3.zero;
    [SerializeField] private AnimationCurve curve;

    private void Awake()
    {      
        originPos = transform.position;
    }

    private void Start()
    {
        time = 0;
        targetPos = SpellManager.Instance.ShootDirection(SpellManager.Instance.ProjectileLayer);
        magnitude = Mathf.Sqrt(Vector3.Dot(targetPos - originPos, targetPos - originPos));

 
        fireball = fireball == 3 ? 1 : fireball + 1;
        switch (fireball)
        {
            case 1:
                projectileOffset = Vector3.left;
                break;
            case 2:
                projectileOffset = Vector3.right;
                break;
            case 3:
                projectileOffset = Vector3.up;
                break;

        }
    }

    private void Update()
    {
        if (time < 1)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, time);
            transform.position += Vector3.Lerp(Vector3.zero,
                                                    projectileOffset * magnitude / 2f * 0.2f,
                                                    curve.Evaluate(time));
            
            time += Time.deltaTime * 1f / magnitude * speed;
        } else
        {
            Destroy(this.gameObject, 0.05f);
        }
    }
}
