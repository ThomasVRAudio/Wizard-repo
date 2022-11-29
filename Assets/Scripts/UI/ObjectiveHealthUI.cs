using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MathFunctions;

public class ObjectiveHealthUI : MonoBehaviour
{
    private RectTransform rect;
    private Image image;
    [SerializeField] private Gradient gradient;
    [SerializeField] private RectTransform border;
    [SerializeField] private EnemyObjective enemyObjective;
    private int currentHealth;
    public static ObjectiveHealthUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        enemyObjective.onDamage += SetHealthBar;
        currentHealth = enemyObjective.Health;
    }

    public void SetHealthBar(int health)
    {
        image.color = gradient.Evaluate(rect.sizeDelta.x / border.sizeDelta.x);
        StartCoroutine(ChangeColor(health));
    }

    private IEnumerator ChangeColor(int health)
    {
        float time = 0;
        while (time < 1f)
        {
            rect.sizeDelta = Vector2.Lerp(new Vector2(MathFunc.Remap(0, 5000, 0, border.sizeDelta.x, currentHealth), rect.sizeDelta.y),
                new Vector2(MathFunc.Remap(0, 5000, 0, border.sizeDelta.x, health), rect.sizeDelta.y), time);
            image.color = gradient.Evaluate(rect.sizeDelta.x / border.sizeDelta.x);
            time += Time.deltaTime * 2f;
            yield return null;
        }

        currentHealth = health;
    }
}
