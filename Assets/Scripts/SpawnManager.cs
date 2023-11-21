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
    public Transform[] spawnPoints;
    private float startDelay = 2;
    public float spawnRate;

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
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dimensionManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
    }

    public void StartSpawnManager()
    {
        StartCoroutine(ZombieSpawn());
        StartCoroutine(PowerUpSpawn());
        StartSpitterSpawn();
        powerUps = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
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
    IEnumerator TrySpawnSpitter()
    {
        yield return new WaitForSeconds(spitter_startDelay);
        EnemySpawner spawner = (((EnemySpawner[])Shuffle(enemySpawnerArray)).FirstOrDefault(enemySpawner => enemySpawner.spawnedEnemy == null));
        if (spawner != null)
        {
            spawner.Spawn(spitterPrefab);
        }
        yield return new WaitForSeconds(spitter_spawnRate);
    }
    public void StartSpitterSpawn()
    {
        enemySpawnerArray = GetComponentsInChildren<EnemySpawner>();
        StartCoroutine(TrySpawnSpitter());
        InvokeRepeating(nameof(TrySpawnSpitter), spitter_startDelay, spitter_spawnRate);
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
}
