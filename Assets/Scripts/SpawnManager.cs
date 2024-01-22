using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    private UpgradesManager upgradesManager;
    private Waves_Dungeon waves_Dungeon;
    public GameObject shooter;
    public GameObject taunter;
    private Transform shooterPos;
    private Transform taunterPos;
    public int enemyCount;
    public int enemyLimit;
    public bool enemyLimitReached;
    public List<GameObject> enemies = new List<GameObject>();
    public bool nextWaveReady = false;
    public bool coroutinesRunning = false;

    //NORMAL ZOMBIES
    public GameObject[] enemyPrefab;
    private int randomSpawnPoint, randomEnemies;
    //[HideInInspector]
    public Transform[] spawnPoints;
    private float startDelay = 3;
    private float spawnRate = 0.7f;

    //SPITTERS
    [HideInInspector]
    public GameObject spitterPrefab;
    [HideInInspector]
    public EnemySpawner[] enemySpawnerArray;
    public float spitter_startDelay = 5;
    private float spitter_spawnRate = 3;

    //EYEBOMBS
    [HideInInspector]
    public GameObject eyeBombPrefab;
    private int eyeSpawnPoint;
    public Transform[] eyeSpawnPoints;

    //NIGHT KNIGHTS
    [HideInInspector]
    public GameObject nightKnightPrefab;
    [HideInInspector]
    public GameObject nightKnightSpawnPoint;

    //BIG BOIS
    [HideInInspector]
    public GameObject bigBoiPrefab;
    [HideInInspector]
    public Transform[] bigSpawnPoints;

    //POWERUP
    [HideInInspector]
    public GameObject powerUpPrefab;
    [HideInInspector]
    public Transform[] powerUpSpawnPoints;
    [HideInInspector]
    public int powerUps;
    [HideInInspector]
    public float powerUp_startDelay = 10;
    [HideInInspector]
    public float powerUp_spawnRate = 10;

    //[BOSS]
    private NightKnight nightKnight;
    private Horse horse;
    public bool nightKnightDead = false;
    public bool horseDead = false;
    [HideInInspector]
    public GameObject brainBoss;
    [HideInInspector]
    public Transform bossSpawnPoint;
    [HideInInspector]
    public float bossStartDelay = 5;
    public bool bossSpawned = false;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        upgradesManager = GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
        waves_Dungeon = GetComponent<Waves_Dungeon>();
        enemyLimitReached = false;
        enemyLimit = 15;
    }

    void Update()
    {
        SpawnHandling();
        UpdateEnemyList();
        enemyCount = enemies.Count;
        if (enemyCount >= enemyLimit)
        {
            enemyLimitReached = true;
        }
        else
        {
            enemyLimitReached = false;
        }
    }
    public void StartSpawnManager()
    {
        //StartCoroutine(ZombieSpawn());
        waves_Dungeon.Wave1();
        StartCoroutine(PowerUpSpawn());
        SpawnPlayers();
        powerUps = 0;
    }
    //New Waves script and reference them from here
    public void SpawnHandling()
    {
        if (gameManager.score >= 100 && !coroutinesRunning)
        {
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            waves_Dungeon.Wave2();
            coroutinesRunning = true;
        }
        if (gameManager.score >= 300 && !coroutinesRunning)
        {
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            waves_Dungeon.Wave3();
            coroutinesRunning = true;
        }
        if (gameManager.score >= 500 && !coroutinesRunning)
        {
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            waves_Dungeon.Wave4();
            coroutinesRunning = true;
        }
        if (gameManager.score >= 750 && !coroutinesRunning)
        {
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            waves_Dungeon.Wave5();
            coroutinesRunning = true;
        }
        if (gameManager.score >= 1000 && !coroutinesRunning)
        {
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            waves_Dungeon.Wave6();
            coroutinesRunning = true;
        }
        if (gameManager.score >= 1300 && !coroutinesRunning)
        {
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            waves_Dungeon.Wave7();
            coroutinesRunning = true;
        }
        if (gameManager.score >= 1600 && !coroutinesRunning)
        {
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            waves_Dungeon.Wave8();
            coroutinesRunning = true;
        }
        if (gameManager.score >= 2000 && !coroutinesRunning)
        {
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            waves_Dungeon.MiniBossWave();
            coroutinesRunning = true;
        }
        if (nightKnight != null)
        {
            if (nightKnight.nightKnightHealth <= 0)
            {
                nightKnightDead = true;
            }   
        }  
        if (nightKnightDead && horseDead && !bossSpawned)
        {
            StartCoroutine(SpawnBrainBoss());
            bossSpawned = true;
        }
    }
    void SpawnPlayers()
    {
        shooterPos = GameObject.Find("ShooterPos").GetComponent<Transform>();
        Instantiate(shooter, shooterPos.position, UnityEngine.Quaternion.identity);
        taunterPos = GameObject.Find("TaunterPos").GetComponent<Transform>();
        Instantiate(taunter, taunterPos.position, UnityEngine.Quaternion.identity);
    }
    public IEnumerator ZombieSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (!bossSpawned && !gameManager.isSomeoneDead)
        {
            if (!enemyLimitReached)
            {
                randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
                randomEnemies = UnityEngine.Random.Range(0, enemyPrefab.Length);
                GameObject zombie = Instantiate(enemyPrefab[randomEnemies], spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
                AddEnemyToList(zombie);
            }           
            yield return new WaitForSeconds(spawnRate);
        }       
    }

    public IEnumerator SpitterSpawn()
    {
        enemySpawnerArray = GetComponentsInChildren<EnemySpawner>();
        if (!gameManager.gameIsPaused)
        {
            yield return new WaitForSeconds(3);
            while (!bossSpawned && !gameManager.isSomeoneDead)
            {
                yield return new WaitForSeconds(spitter_spawnRate);

                EnemySpawner spawner = (((EnemySpawner[])Shuffle(enemySpawnerArray)).FirstOrDefault(enemySpawner => enemySpawner.spitter == null));
                if (spawner != null && !enemyLimitReached)
                {
                    spawner.Spawn(spitterPrefab);
                    AddEnemyToList(spawner.spitter);
                }
            }
        }  
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

    public IEnumerator EyeBombSpawn()
    {   
        yield return new WaitForSeconds(startDelay);
        while (!bossSpawned && !gameManager.isSomeoneDead)
        {
            if (!enemyLimitReached)
            {
                eyeSpawnPoint = UnityEngine.Random.Range(0, eyeSpawnPoints.Length);
                GameObject eyeBomb = Instantiate(eyeBombPrefab, eyeSpawnPoints[eyeSpawnPoint].position, UnityEngine.Quaternion.identity); 
                AddEnemyToList(eyeBomb);
            }
            yield return new WaitForSeconds(7);
        }
    }

    public IEnumerator BigBoiSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (!bossSpawned && !gameManager.isSomeoneDead)
        {
            if (!enemyLimitReached)
            {
                randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
                GameObject bigBoi = Instantiate(bigBoiPrefab, spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
                AddEnemyToList(bigBoi);
            }
            yield return new WaitForSeconds(4);
        }
    }

    public IEnumerator NightKnightSpawn()
    {
        yield return new WaitForSeconds(3);
        Instantiate(nightKnightPrefab, nightKnightSpawnPoint.transform.position, UnityEngine.Quaternion.identity);
        nightKnight = GameObject.FindGameObjectWithTag("NightKnight").GetComponent<NightKnight>();
        horse = GameObject.FindGameObjectWithTag("Horse").GetComponent<Horse>();
    }
    public IEnumerator SpawnBrainBoss()
    {
        gameManager.ClearMap();
        yield return new WaitForSeconds(5);
        Instantiate(brainBoss, bossSpawnPoint.transform.position, UnityEngine.Quaternion.identity);
        
    }
    void AddEnemyToList(GameObject enemy)
    {
        enemies.Add(enemy);
    }
    public void UpdateEnemyList()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            // Check if the enemy GameObject is null (destroyed)
            if (enemies[i] == null)
            {
                // Enemy has been destroyed
                Debug.Log("Enemy " + i + " has been destroyed.");

                // Optionally, you can remove the destroyed enemy from the list
                enemies.RemoveAt(i);
                //Spitter is not getting removed
            }
        }
    }
}