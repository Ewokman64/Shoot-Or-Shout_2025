using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    private UpgradesManager upgradesManager;
    public GameObject shooter;
    public GameObject taunter;
    private Transform shooterPos;
    private Transform taunterPos;
    public int enemyCount;
    public int enemyLimit;
    public bool enemyLimitReached;
    public List<GameObject> enemies = new List<GameObject>();
    //public bool nextWaveReady = false;

    //MOB CHECKS
    public bool newWaveStarted;

    //NIGHT KNIGHTS
    [HideInInspector]
    public GameObject nightKnightPrefab;
    [HideInInspector]
    public GameObject nightKnightSpawnPoint;

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

    public SpawnFillerEnemies spawnFillerEnemies;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        upgradesManager = GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
        spawnFillerEnemies = GameObject.Find("SpawnManagerNew").GetComponent<SpawnFillerEnemies>();
        enemyLimitReached = false;
        enemyLimit = 15;
    }

    void Update()
    {
        SpawnHandling();
        //UpdateEnemyList();
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
        StartCoroutine(spawnFillerEnemies.SpawnWaves());
        StartCoroutine(spawnFillerEnemies.SpawnSpecialWaves());
        StartCoroutine(spawnFillerEnemies.SpawnRanged());
        StartCoroutine(PowerUpSpawn());
        SpawnPlayers();
        powerUps = 0;
    }
    //New Waves script and reference them from here
    public void SpawnHandling()
    {
        
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
    IEnumerator PowerUpSpawn()
    {
        while (powerUps == 0)
        {
            yield return new WaitForSeconds(powerUp_startDelay);

            int randomSpawnPoint;

            randomSpawnPoint = UnityEngine.Random.Range(0, powerUpSpawnPoints.Length);
            Instantiate(powerUpPrefab, powerUpSpawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);

            powerUps = 1;
            yield return new WaitForSeconds(powerUp_spawnRate);
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
    /*public void AddEnemyToList(GameObject enemy)
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
    }*/
}