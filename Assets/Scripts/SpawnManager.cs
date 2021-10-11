using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    private Difficulty difficulty;
    public GameObject[] enemyPrefab;
    public GameObject powerUpPrefab;
    public Transform[] spawnPoints;
    public Transform[] powerUpSpawnPoints;
    int randomSpawnPoint, randomEnemies;
    public static int powerUps;
    private float startDelay = 2;
    public float spawnRate;
    [HideInInspector]
    public float p_up_startDelay = 10;
    [HideInInspector]
    public float p_up_spawnRate = 10;
    void Start()
    {
        difficulty = GameObject.Find("Difficulty Settings").GetComponent<Difficulty>();
    }

    public void StartSpawnManager()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating(nameof(EnemySpawn), startDelay, spawnRate);
        InvokeRepeating(nameof(PowerUpSpawn), p_up_startDelay, p_up_spawnRate);
        powerUps = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (powerUps >= 1)
        {
            CancelInvoke("PowerUpSpawn");
        }
        if (gameManager.isSomeoneDead == true)
        {
            gameManager.GameOver();
            CancelInvoke("EnemySpawn");
            CancelInvoke("PowerUpSpawn");
        }
    }
    void EnemySpawn()
    {
        randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        randomEnemies = UnityEngine.Random.Range(0, enemyPrefab.Length);
        Instantiate(enemyPrefab[randomEnemies], spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
    }

    public void PowerUpSpawn()
    {
        randomSpawnPoint = UnityEngine.Random.Range(0, powerUpSpawnPoints.Length);
        Instantiate(powerUpPrefab, powerUpSpawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
        ++powerUps;
    }
    public void SetPowerUpBack()
    {
        powerUps = 0;
        InvokeRepeating(nameof(PowerUpSpawn), p_up_startDelay, p_up_spawnRate);
    }
}
