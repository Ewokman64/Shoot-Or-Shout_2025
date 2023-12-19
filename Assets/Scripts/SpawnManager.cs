using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
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
    //FINAL PUSH
    //TRANSFORMING INTO STRONGER ZOMBIES - THE FINAL PUSH
    public GameObject finalPush_zombie;
    public GameObject finalPush_spitter;
    public GameObject finalPush_eyeBomb;
    public GameObject finalPush_bigBoi;
    public GameObject finalPush_nightKnight;
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
        /*if (gameManager.score >= 10 && !spitterSpawnStarted)
        {
            StartCoroutine(SpitterSpawn());
            //StartSpitterSpawn();
            spitterSpawnStarted = true;
        }
        if (gameManager.score >= 15 && !eyeBombSpawnStarted)
        {
            StartCoroutine(EyeBombSpawn());
            StartCoroutine(BigBoiSpawn());
            bigBombSpawnStarted = true;
            eyeBombSpawnStarted = true;
        }
        if (gameManager.score >= 50 && !n_KnightSpawnStarted)
        {
            Debug.Log("Condition met! Miniboss incoming!");
            StartCoroutine(NightKnightSpawn());
            n_KnightSpawnStarted = true;
        }*/
        if (gameManager.score >= 10 && !bossSpawned)
        {
            //StartCoroutine(SpawnBrainBoss());
            StartCoroutine(FinalPush());
            bossSpawned = true;
        }
        
    }
    IEnumerator ZombieSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (!bossSpawned && !gameManager.isSomeoneDead)
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

        while (!bossSpawned)
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
        yield return new WaitForSeconds(startDelay);
        while (!bossSpawned)
            {
            eyeSpawnPoint = UnityEngine.Random.Range(0, eyeSpawnPoints.Length);
            
            Instantiate(eyeBombPrefab, eyeSpawnPoints[eyeSpawnPoint].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(7);
        }
    }

    IEnumerator BigBoiSpawn()
    {
        Debug.Log("Initiating eyebombspawn");
        yield return new WaitForSeconds(startDelay);
        while (!bossSpawned)
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
        gameManager.ClearMap();
        yield return new WaitForSeconds(5);
        Instantiate(brainBoss, bossSpawnPoint.transform.position, UnityEngine.Quaternion.identity);
        
    }
    public IEnumerator FinalPush()
    {
        // Boss turning into each enemy for a very short time
        GameObject finalPushEnemy;
        // turns into buffed zombie
        finalPushEnemy = Instantiate(finalPush_zombie, transform.position, Quaternion.identity);
        Debug.Log("Zombie is here");
        yield return new WaitForSeconds(5);
        Debug.Log("5 seconds passed");
        Destroy(finalPushEnemy);

        // turns into buffed spitter
        finalPushEnemy = Instantiate(finalPush_spitter, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        Destroy(finalPushEnemy);

        // turns into buffed eyeBomb
        finalPushEnemy = Instantiate(finalPush_eyeBomb, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        Destroy(finalPushEnemy);

        // turns into buffed bigBoi
        finalPushEnemy = Instantiate(finalPush_bigBoi, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        Destroy(finalPushEnemy);

        // turns into buffed nightKnight
        finalPushEnemy = Instantiate(finalPush_nightKnight, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5);
        Destroy(finalPushEnemy);
        
        // Fokhen dies, fireworks and all
    }
}