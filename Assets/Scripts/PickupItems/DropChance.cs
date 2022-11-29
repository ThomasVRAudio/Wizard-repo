using System.Collections;
using System.Linq;
using UnityEngine;

public class DropChance : MonoBehaviour
{
    public static DropChance Instance { get; private set; }
    [SerializeField] private GameObject SkillItem;
    [SerializeField] private GameObject SpeedItem;
    [SerializeField] private GameObject DamageItem;
    [SerializeField] private GameObject HealthItem;
    private const int maxSkillItems = 5;
    private static int skillItems = 0;
    [SerializeField] private GameObject[] AlwaysDropEnemies;
    void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void Chance(GameObject enemy)
    {
        int number = Random.Range(1, 1001);
        
        switch (true)
        {
            case true when number >= 850 && number < 890:
                DropItem(HealthItem, enemy);
                break;
            case true when number >= 890 && number < 900:
                DropItem(SpeedItem, enemy);
                break;
            case true when number >= 900 && number < 910:
                DropItem(DamageItem, enemy);
                break;
            case true when number >= 999:
                DropItem(SkillItem, enemy);
                break;
            default:
                DropItem(null, enemy);
                break;
        }           
    }

    private void DropItem(GameObject item, GameObject enemy)
    {
        if ((item == null || item == HealthItem) && enemy.CompareTag("BigEnemy"))
        {
             Chance(enemy);
             return;
        }

        if (item == null)
            return;

        if (item == SkillItem)
        {
            if(skillItems >= maxSkillItems)
            {
                Chance(enemy);
            } else
            {
                skillItems++;
            }
        }

        Instantiate(item, enemy.transform.position + Vector3.up * 2, Quaternion.identity);
    }
}
