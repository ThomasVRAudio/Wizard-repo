using UnityEngine;

public class EarthSpellObject : ProjectileSpellObject
{
    private float time = 0;
    private Vector3 originPos;
    private Vector3 targetPos;
    private float magnitude = 0;
    private const float speed = 30f;

    private void Awake()
    {      
        originPos = transform.position;
    }

    private void Start()
    {
        time = 0;
        targetPos = SpellManager.Instance.ShootDirection(SpellManager.Instance.ProjectileLayer);
        magnitude = Mathf.Sqrt(Vector3.Dot(targetPos - originPos, targetPos - originPos));
    }

    private void Update()
    {
        if (time < 1)
        {
            transform.position = Vector3.Lerp(originPos, targetPos, time);        
            time += Time.deltaTime * 1f / magnitude * speed;
        } else
        {
            Destroy(this.gameObject, 1f);
            CameraShake.Instance.ShakeCamera(1f, 0.1f);
        }

        transform.Rotate(new Vector3(1,1,1));
    }
}
