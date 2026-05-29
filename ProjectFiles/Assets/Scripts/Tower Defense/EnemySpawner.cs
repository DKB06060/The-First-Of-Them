using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner main;

    [Header("References")]
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] Text waveUI;

    [Header("Configurables")]
    [SerializeField] int baseEnemies = 8;
    [SerializeField] float enemiesPerSecond = 0.5f;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float difficultyScalingFactor = 0.75f;
    [SerializeField] float enemiesPerSecondCap = 10;

    [Header("Events")]
    [SerializeField] public static UnityEvent onEnemyDestroy = new UnityEvent();

    [HideInInspector] public int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private int enemySpawnIndex;
    private float eps;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        main = this;
    }

    private void Start()
    {
        StartCoroutine(StartWave());
    }

    private void Update()
    {
        waveUI.text = "Wave: " + currentWave;

        if (!isSpawning) return;
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0) 
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();

            if (currentWave >= 12)
            {
                LevelManager.main.WinGame();
            }
        }

        if (LevelManager.main.lives <= 0)
        {
            LevelManager.main.LoseGame();
        }
    }

    void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
        eps = EnemiesPerSecond();
    }

    int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave());
    }

    float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    void SpawnEnemy()
    {
        if (currentWave <= 5)
        {
            enemySpawnIndex = Random.Range(0, 7);
        }
        else if (currentWave > 5 && currentWave <= 10)
        {
            enemySpawnIndex = Random.Range(0, 10);
        }
        else if (currentWave > 10)
        {
            enemySpawnIndex = Random.Range(0, enemyPrefabs.Length);
        }
        GameObject prefabToSpawn = enemyPrefabs[enemySpawnIndex];
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);
    }
}