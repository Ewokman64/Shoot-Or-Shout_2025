﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject shooter;
    public GameObject taunter;
    private Transform shooterPos;
    private Transform taunterPos;

    //NORMAL ZOMBIES
    public GameObject[] enemyPrefab;
    private int randomSpawnPoint, randomEnemies;
    [HideInInspector]
    public Transform[] spawnPoints;
    private float startDelay = 2;
    private float spawnRate = 0.7f;

    //SPITTERS
    [HideInInspector]
    public GameObject spitterPrefab;
    [HideInInspector]
    public EnemySpawner[] enemySpawnerArray;
    public float spitter_startDelay = 5;
    private float spitter_spawnRate = 3;
    public bool spitterSpawnStarted = false;

    //EYEBOMBS
    [HideInInspector]
    public GameObject eyeBombPrefab;
    private int eyeSpawnPoint;
    public Transform[] eyeSpawnPoints;
    public bool eyeBombSpawnStarted = false;

    //NIGHT KNIGHTS
    [HideInInspector]
    public GameObject nightKnightPrefab;
    [HideInInspector]
    public GameObject nightKnightSpawnPoint;
    public bool n_KnightSpawnStarted = false;

    //BIG BOIS
    [HideInInspector]
    public GameObject bigBoiPrefab;
    [HideInInspector]
    public Transform[] bigSpawnPoints;
    public bool bigBoiSpawnStarted = false;

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

    //FINAL PUSH
    public bool finalPushCanStart = false;
    public bool finalPushOver = false;
    [HideInInspector]
    public GameObject finalPush_zombie;
    [HideInInspector]
    public GameObject finalPush_spitter;
    [HideInInspector]
    public GameObject finalPush_eyeBomb;
    [HideInInspector]
    public GameObject finalPush_bigBoi;
    [HideInInspector]
    public GameObject finalPush_nightKnight;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        SpawnHandling();
    }
    public void StartSpawnManager()
    {
        StartCoroutine(ZombieSpawn());
        StartCoroutine(PowerUpSpawn());
        SpawnPlayers();
        powerUps = 0;
    }
    public void SpawnHandling()
    {
        if (gameManager.score >= 50 && !spitterSpawnStarted)
        {
            StartCoroutine(SpitterSpawn());
            //StartSpitterSpawn();
            spitterSpawnStarted = true;
        }
        if (gameManager.score >= 100 && !eyeBombSpawnStarted)
        {
            StartCoroutine(EyeBombSpawn());
            eyeBombSpawnStarted = true;
        }
        if (gameManager.score >= 150 && !bigBoiSpawnStarted)
        {
            StartCoroutine(BigBoiSpawn());
            bigBoiSpawnStarted = true;
        }
        if (gameManager.score >= 300 && !n_KnightSpawnStarted)
        {
            StartCoroutine(NightKnightSpawn());
            n_KnightSpawnStarted = true;
            nightKnight = GameObject.FindGameObjectWithTag("NightKnight").GetComponent<NightKnight>();
            horse = GameObject.FindGameObjectWithTag("Horse").GetComponent<Horse>();
        }
        if (nightKnight != null)
        {
            if (nightKnight.nightKnightHealth < 0)
            {
                nightKnightDead = true;
            }   
        }
        if (horse != null)
        {
            if (horse.horseHealth < 0 && horse != null)
            {
                horseDead = true;
            }
        }
        if (nightKnightDead && horseDead && !bossSpawned)
        {
            StartCoroutine(SpawnBrainBoss());
            bossSpawned = true;
        }
        //IF SECONDWAVEBOSS DIED, start FINALPUSH
        if (finalPushCanStart)
        {
            StartCoroutine(FinalPush());
            finalPushCanStart = false;
        }
    }

    void SpawnPlayers()
    {
        shooterPos = GameObject.Find("ShooterPos").GetComponent<Transform>();
        Instantiate(shooter, shooterPos.position, UnityEngine.Quaternion.identity);
        taunterPos = GameObject.Find("TaunterPos").GetComponent<Transform>();
        Instantiate(taunter, taunterPos.position, UnityEngine.Quaternion.identity);
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
        yield return new WaitForSeconds(5);
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

        yield return new WaitForSeconds(5);
        finalPushOver = true;
        // Fokhen dies, fireworks and all
    }
}