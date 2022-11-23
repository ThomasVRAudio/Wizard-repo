using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject Orc;
    [SerializeField] private GameObject Goblin;
    [SerializeField] private GameObject[] Portal;
    [SerializeField] private float SpawnSpeed = 0.3f;
    [SerializeField] private GameObject Objective;

    private List<GameObject> trackedEnemies = new List<GameObject> ();
    public static SpawnManager Instance { get; private set; }
    public delegate void OnZeroEnemies();
    public OnZeroEnemies OnZeroEnemiesCallback;

    private int orcNumber = 0;
    private int goblinNumber = 0;
    private int amountToSpawn = 0;
    private int subtractAmount = 1;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public void StartWave(int wave)
    {
        SetSpawnAmount(wave);
        StartSpawners();
    }

    private void SetSpawnAmount(int wave)
    {
        goblinNumber = wave * 5;

        if (wave % 3 == 0)
        {
            orcNumber = wave / 3;
        }
        else { orcNumber = 0; }
    }

    private void StartSpawners()
    {
        amountToSpawn = goblinNumber + orcNumber;

        int goblinRemainder = goblinNumber % Portal.Length;
        int orcRemainder = orcNumber % Portal.Length;
        int restOrc = orcRemainder;

        for (int i = 0; i < Portal.Length; i++)
        {
            if (i != 0)
                goblinRemainder = 0;

            switch (restOrc)
            {
                case 0:
                    orcRemainder = 0;
                    break;
                case 1:
                    orcRemainder = i == 0 ? 1 : 0;
                    break;
                case 2:
                    orcRemainder = i == 2 ? 0 : 1;
                    break;
                default:
                    orcRemainder = 0;
                    break;
            }

            StartCoroutine(Spawner(goblinNumber / Portal.Length + goblinRemainder, Goblin, Portal[i]));
            StartCoroutine(Spawner(orcNumber / Portal.Length + orcRemainder, Orc, Portal[i]));
        }
    }

    private IEnumerator Spawner(int x, GameObject enemy, GameObject portal)
    {
        if (x <= 0)
            yield break;

        subtractAmount = true switch
        {
            true when enemy == Goblin => 3,
            true when enemy == Orc => 1,
            _ => 1,
        };

        yield return new WaitForSeconds(SpawnSpeed);
        SpawnEnemy(Mathf.Clamp(subtractAmount, 0, x), enemy, portal);
        StartCoroutine(Spawner(Mathf.Clamp(x - subtractAmount, 0, int.MaxValue), enemy, portal));
    }

    private void SpawnEnemy(int amount, GameObject enemy, GameObject portal)
    {

        Transform SpawnPos = portal.GetComponent<Portal>().SpawnPos.transform;
       
        for(int i = 1; i <= amount; i++)
        {
            Vector3 dir = true switch
            {
                true when i == 1 => Vector3.zero,
                true when i == 2 => Vector3.right,
                true when i == 3 => Vector3.left,
                _ => Vector3.zero,
            };

            enemy = Instantiate(enemy, SpawnPos.transform.rotation * (dir * 0.2f) + SpawnPos.position, Quaternion.identity);
            enemy.GetComponent<EnemyStateManager>().SetObjective(Objective);
            trackedEnemies.Add(enemy);
        }
    }

    public void RemoveFromList(GameObject enemy)
    {
        amountToSpawn--;
        trackedEnemies.Remove(enemy);
        if (trackedEnemies.Count <= 0 && amountToSpawn <= 0)
            OnZeroEnemiesCallback?.Invoke();
    }
}