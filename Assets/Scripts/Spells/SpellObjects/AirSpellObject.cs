using UnityEngine;

public class AirSpellObject : SpellCreation
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private AnimationCurve visibilityCurve;
    [SerializeField] private Material shaderMaterial;
    [SerializeField] float speed = 0.2f;
    private float visibility;
    private float time = 0;

    [SerializeField] private int damage = 100;

    void Update()
    {
        if(time < 1)
        {
            gameObject.transform.localScale = Vector3.Lerp(new Vector3(0,0,0), new Vector3(30, 30, 30), curve.Evaluate(time));
            visibility = Mathf.Lerp(0, 1, visibilityCurve.Evaluate(time));
            shaderMaterial.SetFloat("Visibility_", visibility);
            time += Time.deltaTime * speed;
        } else
        {
            Destroy(gameObject);
        }
    }

    protected override void SetDamage(Collider target)
    {
        target.gameObject.GetComponent<IDamageable>().OnHit(damage + baseDamage);
    }
}
