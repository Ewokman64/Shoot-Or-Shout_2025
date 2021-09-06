using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject[] enemyPrefab;
    public GameObject spitterPrefab;
    public GameObject powerUpPrefab;
    public Transform[] spawnPoints;
    public Transform[] powerUpSpawnPoints;
    int randomSpawnPoint, randomEnemies;
    private static int powerUps;
    public static int spitters;
    private float startDelay = 2;
    private float spawnRate = 1;
    private float spitter_StartDelay = 5;
    private float spitter_SpawnRate = 3;
    private float p_up_startDelay = 10;
    private float p_up_spawnRate = 10;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating(nameof(EnemySpawn), startDelay, spawnRate);
        InvokeRepeating(nameof(SpitterSpawn), spitter_StartDelay, spitter_SpawnRate);
        InvokeRepeating(nameof(PowerUpSpawn), p_up_startDelay, p_up_spawnRate);
        powerUps = 0;
        spitters = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (powerUps >= 1)
            CancelInvoke("PowerUpSpawn");
        if (spitters >= 1)
            CancelInvoke("SpitterSpawn");
        if (gameManager.isSomeoneDead == true)
        {
            gameManager.GameOver();
            CancelInvoke("EnemySpawn");
            CancelInvoke("SpitterSpawn");
            CancelInvoke("PowerUpSpawn");
        }
    }
    void EnemySpawn()
    {
        randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        randomEnemies = UnityEngine.Random.Range(0, enemyPrefab.Length);
        Instantiate(enemyPrefab[randomEnemies], spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
    }
    void SpitterSpawn()
    {
        randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        Instantiate(spitterPrefab, spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
        ++spitters;
    }

    void PowerUpSpawn()
    {
        randomSpawnPoint = UnityEngine.Random.Range(0, powerUpSpawnPoints.Length);
        Instantiate(powerUpPrefab, powerUpSpawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
        ++powerUps;
    }
}
