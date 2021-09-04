using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    private float startDelay = 2;
    private float spawnRate = 1;
    private float spitter_StartDelay = 5;
    private float spitter_SpawnRate = 3;
    private float p_up_startDelay = 10;
    private float p_up_spawnRate = 10;
    private Bullet bullet;
    public GameObject[] enemyPrefab;
    public GameObject spitterPrefab;
    public GameObject powerUpPrefab;
    public GameObject restartButton;
    public GameObject restartMenu;  
    public AudioSource gmAudio;
    public AudioClip zombie;
    public Transform[] spawnPoints;
    public Transform[] powerUpSpawnPoints;
    public TextMeshProUGUI soulEnergyText;
    public TextMeshProUGUI soulEnergyCollectedText;
    public TextMeshProUGUI currentRecordText;
    int randomSpawnPoint, randomEnemies;
    public int score;
    public int currentRecord;
    public bool isSomeoneDead = false;
    public bool isTaunterChased;
    public bool isShooterChased;
    private static int powerUps;
    public static int spitters;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        ScoreManager();
        if (powerUps >= 1)
            CancelInvoke("PowerUpSpawn");
        if (spitters >= 1)
            CancelInvoke("SpitterSpawn");
    }
    void StartGame()
    {
        powerUps = 0;
        spitters = 0;
        currentRecord = PlayerPrefs.GetInt("currentRecord", 0);
        InvokeRepeating(nameof(EnemySpawn), startDelay, spawnRate);
        InvokeRepeating(nameof(SpitterSpawn), spitter_StartDelay, spitter_SpawnRate);
        InvokeRepeating(nameof(PowerUpSpawn), p_up_startDelay, p_up_spawnRate);
        isTaunterChased = false;
        isShooterChased = true;
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
    
    public void GameOver()
    {
        if (currentRecord < score)
        {
            PlayerPrefs.SetInt("currentRecord", score);
        }      
        restartButton.SetActive(true);
        restartMenu.SetActive(true);
        soulEnergyCollectedText.text = "Soul Energy Collected: " + score;
        CancelInvoke("EnemySpawn");
        DestroyEnemies();
        gmAudio.mute = true;
    }
    public void ZombieSound()
    {
        gmAudio.PlayOneShot(zombie, 1.0f);
    }
    public void UpdateCurrency(int currencyToAdd)
    {
        score += currencyToAdd;
        soulEnergyText.text = "Soul Energy: " + score;
    }

    public void DestroyEnemies()

    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject enemy in enemies)
        Destroy(enemy);
    }

    public void ScoreManager()
    {
        if (currentRecord < score) currentRecordText.text = "Current Record: " + score.ToString();
        else currentRecordText.text = "Current Record: " + currentRecord.ToString();
    }
}


