using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI _TextMeshProUGUI;
    [SerializeField] private int wave = 1;

    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        SpawnManager.Instance.StartWave(wave);
        SpawnManager.Instance.OnZeroEnemiesCallback += OnZeroEnemies;
    }

    private void OnZeroEnemies()
    {
        wave++; 
        StartCoroutine(TimeToNextWave());
    }

    private IEnumerator TimeToNextWave()
    {
        yield return new WaitForSeconds(5f);
        StartNextWave();
    }
    private void StartNextWave() 
    {
        SpawnManager.Instance.StartWave(wave);
        _TextMeshProUGUI.text = $"Wave: {wave}";
    }

    public void OnLose()
    {
        Debug.Log("You Lost");
    }
  
}
