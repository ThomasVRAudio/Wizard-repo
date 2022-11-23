using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public int BaseHealth { get; private set; } = 100;
    public int BaseDamage { get; private set; } = 0;
    public float BaseSpeed { get; private set; } = 2;

    private int skillCount = 0;

    private Player player;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);

        }
        else
        {
            Instance = this;
        }

        player = GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickupItem>() == null) return;

        other.gameObject.GetComponent<Animator>().SetTrigger("PickUp");

        PickupItem.PickupType itemType = other.GetComponent<PickupItem>().itemType;

        if (itemType == PickupItem.PickupType.speed)
        {
            BaseSpeed += 0.5f;
            player.UpdateSpeed(BaseSpeed);
        }

        if (itemType == PickupItem.PickupType.damage)
            BaseDamage += 10;

        if (itemType == PickupItem.PickupType.skill)
            AddSkill();
    }

    private void AddSkill()
    {
        switch (skillCount)
        {
            case 0:
                if(gameObject.GetComponent<EarthSpell>() == null)
                    gameObject.AddComponent<EarthSpell>();
                break;
            case 1:
                if (gameObject.GetComponent<TornadoSpell>() == null)
                    gameObject.AddComponent<TornadoSpell>();
                break;
            case 2:
                if (gameObject.GetComponent<AirSpell>() == null)
                    gameObject.AddComponent<AirSpell>();
                break;
            default:
                break;
        }

        skillCount++;
    }
}
