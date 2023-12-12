using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public BrainBossHealthBar healthBar;
    //NORMAL ZOMBIES
    public GameObject[] enemyPrefab;
    int randomSpawnPoint, randomEnemies;
    public Transform[] spawnPoints;
    private float startDelay = 2;
    public float spawnRate = 0.7f;
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
    public GameObject nightKnightPrefab;
    public GameObject nightKnightSpawnPoint;
    public bool n_KnightSpawnStarted = false;
    //BIG LADS
    public GameObject bigBoiPrefab;
    int bigSpawnPoint;
    public Transform[] bigSpawnPoints;
    public bool bigBombSpawnStarted = false;
    //POWERUP
    public GameObject powerUpPrefab;
    public Transform[] powerUpSpawnPoints;
    [HideInInspector]
    public int powerUps;
    [HideInInspector]
    public float powerUp_startDelay = 10;
    [HideInInspector]
    public float powerUp_spawnRate = 10;
    //[BOSS]
    public GameObject brainBoss;
    public Transform bossSpawnPoint;
    public float bossStartDelay = 5;
    public bool bossSpawned = false;
    //BOSS SECOND PHASE
    public GameObject brainBoss_2nd;
    public bool secondPhaseStarted = false;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (gameManager.score >= 50 && !spitterSpawnStarted)
        {
            StartCoroutine(SpitterSpawn());
            //StartSpitterSpawn();
            spitterSpawnStarted = true;
        }
        if (gameManager.score >= 20 && !eyeBombSpawnStarted)
        {
            StartCoroutine(EyeBombSpawn());
            StartCoroutine(BigBoiSpawn());
            bigBombSpawnStarted = true;
            eyeBombSpawnStarted = true;
        }
        if (gameManager.score >= 70 && !n_KnightSpawnStarted)
        {
            Debug.Log("Condition met! Miniboss incoming!");
            StartCoroutine(NightKnightSpawn());
            n_KnightSpawnStarted = true;
        }
        if (gameManager.score >= 500 && !bossSpawned)
        {
            StartCoroutine(SpawnBrainBoss());
            bossSpawned = true;
        }
        if (healthBar.currentHealth <= 0 && !secondPhaseStarted)
        {
            StartCoroutine(BossSecondPhase());
            secondPhaseStarted = true;
        }
        else if (healthBar.currentHealth <= 0 && secondPhaseStarted)
        {
            Destroy(brainBoss_2nd);
        }
    }
    IEnumerator ZombieSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (true && !n_KnightSpawnStarted)
        {
            randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
            randomEnemies = UnityEngine.Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemies], spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }
        
    }
    IEnumerator SpitterSpawn()
    {
        enemySpawnerArray = GetComponentsInChildren<EnemySpawner>();

        while (true && !n_KnightSpawnStarted)
        {
            yield return new WaitForSeconds(spitter_spawnRate);

            EnemySpawner spawner = (((EnemySpawner[])Shuffle(enemySpawnerArray)).FirstOrDefault(enemySpawner => enemySpawner.spawnedEnemy == null));
            if (spawner != null)
            {
                spawner.Spawn(spitterPrefab);
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

    IEnumerator EyeBombSpawn()
    {
        Debug.Log("Initiating eyebombspawn");
        yield return new WaitForSeconds(startDelay);
        while (true && !n_KnightSpawnStarted)
        {
            Debug.Log("EyeBomb spawn started!");
            eyeSpawnPoint = UnityEngine.Random.Range(0, eyeSpawnPoints.Length);
            
            Instantiate(eyeBombPrefab, eyeSpawnPoints[eyeSpawnPoint].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(7);
        }
    }

    IEnumerator BigBoiSpawn()
    {
        Debug.Log("Initiating eyebombspawn");
        yield return new WaitForSeconds(startDelay);
        while (true && !n_KnightSpawnStarted)
        {
            Debug.Log("EyeBomb spawn started!");
            randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);

            Instantiate(bigBoiPrefab, spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator NightKnightSpawn()
    {
        Debug.Log("Night Knight is coming!");
        StopCoroutine("ZombieSpawn");
        StopCoroutine("SpitterSpawn");        
        StopCoroutine("EyeBombSpawn");
        StopCoroutine("BigBoiSpawn");
        yield return new WaitForSeconds(3);
        Instantiate(nightKnightPrefab, nightKnightSpawnPoint.transform.position, UnityEngine.Quaternion.identity);
    }
    IEnumerator SpawnBrainBoss()
    {
        StopAllCoroutines();
        yield return new WaitForSeconds(5);
        Instantiate(brainBoss, bossSpawnPoint.transform.position, UnityEngine.Quaternion.identity);
    }

    public IEnumerator BossSecondPhase()
    {
        yield return new WaitForSeconds(2);
        Destroy(brainBoss);
        yield return new WaitForSeconds(3);
        Instantiate(brainBoss_2nd, bossSpawnPoint.transform.position, UnityEngine.Quaternion.identity);
    }
}