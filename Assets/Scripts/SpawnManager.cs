using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    private DimensionManager dimensionManager;
    //NORMAL ZOMBIES
    public GameObject[] enemyPrefab;
    int randomSpawnPoint, randomEnemies;
    public Transform[] spawnPoints;
    private float startDelay = 2;
    public float spawnRate = 0.7f;
    private bool zombieSpawnStarted = false;
    //SPITTERS
    public GameObject spitterPrefab;
    public EnemySpawner[] enemySpawnerArray;
    public float spitter_startDelay = 5;
    public float spitter_spawnRate = 3;
    private bool spitterSpawnStarted = false;
    //EYEBOMBS
    public GameObject eyeBombPrefab;
    int eyeSpawnPoint;
    public Transform[] eyeSpawnPoints;
    public bool eyeBombSpawnStarted = false;
    //NIGHT KNIGHTS
    


    //BIG LADS

    //POWERUP
    public GameObject powerUpPrefab;
    public Transform[] powerUpSpawnPoints;
    [HideInInspector]
    public int powerUps;
    [HideInInspector]
    public float powerUp_startDelay = 10;
    [HideInInspector]
    public float powerUp_spawnRate = 10;
    
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
        if (gameManager.score > 50 && !spitterSpawnStarted)
        {
            StartSpitterSpawn();
            spitterSpawnStarted = true;
        }
        if (!zombieSpawnStarted)
        {

        }
        if (gameManager.score > 20 && !eyeBombSpawnStarted)
        {
            StartCoroutine(EyeBombSpawn());
            eyeBombSpawnStarted = true;
        }
    }
    IEnumerator ZombieSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
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
        Debug.Log("Initiating eyebombspawn");
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            Debug.Log("EyeBomb spawn started!");
            randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
            
            Instantiate(eyeBombPrefab, spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(7);
        }
    }
}
