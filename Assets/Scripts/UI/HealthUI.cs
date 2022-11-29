using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using MathFunctions;

public class HealthUI : MonoBehaviour
{
    private RectTransform rect;
    private Image image;
    [SerializeField] private Gradient gradient;
    [SerializeField] private RectTransform border;
    [SerializeField] private PlayerHealth playerHealth;
    private int currentHealth;
    public static HealthUI Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    void Start()
    {
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        playerHealth.onDamage += SetHealthBar;
        currentHealth = playerHealth.Health;
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
            rect.sizeDelta = Vector2.Lerp(new Vector2(MathFunc.Remap(0, 100, 0, border.sizeDelta.x, currentHealth), rect.sizeDelta.y), 
                new Vector2(MathFunc.Remap(0, 100, 0, border.sizeDelta.x, health), rect.sizeDelta.y), time);
            image.color = gradient.Evaluate(rect.sizeDelta.x / border.sizeDelta.x);
            time += Time.deltaTime * 2f;
            yield return null;
        }

        currentHealth = health;
    }
}
