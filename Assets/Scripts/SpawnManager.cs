using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    private DimensionManager dimensionManager;
    //----------------------------------------
    public GameObject[] enemyPrefab;
    public GameObject powerUpPrefab;
    public GameObject eyeBombPrefab;
    public Transform[] spawnPoints;
    private float startDelay = 2;
    public float spawnRate = 10;

    int randomSpawnPoint, randomEnemies;

    public Transform[] powerUpSpawnPoints;

    [HideInInspector]
    public float powerUp_startDelay = 10;
    [HideInInspector]
    public float powerUp_spawnRate = 10;

    public int powerUps;

    public GameObject spitterPrefab;
    public GameObject cannonYetiPrefab;
    public EnemySpawner[] enemySpawnerArray;
    [HideInInspector]
    public float spitter_startDelay = 5;
    [HideInInspector]
    public float spitter_spawnRate = 3;

    private bool zombieSpawnStarted = false;
    private bool coroutineStarted = false;
    private bool eyeBombSpawnStarted = false;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dimensionManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
    }

    public void StartSpawnManager()
    {
        StartCoroutine(ZombieSpawn());
        StartCoroutine(PowerUpSpawn());
        
        powerUps = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameManager.score > 50 && !coroutineStarted)
        {
            StartSpitterSpawn();
            coroutineStarted = true;
        }
        if (!zombieSpawnStarted)
        {

        }
        if (gameManager.score > 100 && !eyeBombSpawnStarted)
        {
            EyeBombSpawn();
            eyeBombSpawnStarted = true;
        }
    }
    IEnumerator ZombieSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (dimensionManager.DungeonDimension.activeSelf)
        {
            randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
            randomEnemies = UnityEngine.Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemies], spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }
        
    }
    IEnumerator SpawnSpittersWithDelay()
    {
        enemySpawnerArray = GetComponentsInChildren<EnemySpawner>();

        while (true)
        {
            yield return new WaitForSeconds(spitter_spawnRate);

            EnemySpawner spawner = (((EnemySpawner[])Shuffle(enemySpawnerArray)).FirstOrDefault(enemySpawner => enemySpawner.spawnedEnemy == null));
            if (spawner != null)
            {
                spawner.Spawn(spitterPrefab);
            }
        }
    }
    public void StartSpitterSpawn()
    {
        StartCoroutine(SpawnSpittersWithDelay());
    }

    object[] Shuffle(object[] array)
    {
        System.Random random = new System.Random();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            object temp = array[i];
            array[i] = array[swapIndex];
            array[swapIndex] = temp;
        }
        return array;
    }
    IEnumerator PowerUpSpawn()
    {       
        while (powerUps == 0)
        {
            yield return new WaitForSeconds(powerUp_startDelay);

            randomSpawnPoint = UnityEngine.Random.Range(0, powerUpSpawnPoints.Length);
            Instantiate(powerUpPrefab, powerUpSpawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
            powerUps = 1;
            yield return new WaitForSeconds(powerUp_spawnRate);
        }      
    }

    IEnumerator EyeBombSpawn()
    {
        enemySpawnerArray = GetComponentsInChildren<EnemySpawner>();

        while (true)
        {
            yield return new WaitForSeconds(spitter_spawnRate);

            EnemySpawner spawner = (((EnemySpawner[])Shuffle(enemySpawnerArray)).FirstOrDefault(enemySpawner => enemySpawner.spawnedEnemy == null));
            if (spawner != null)
            {
                spawner.Spawn(eyeBombPrefab);
            }
        }
    }
}
