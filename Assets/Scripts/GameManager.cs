using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    private float startDelay = 2;
    private float spawnRate = 1.0f;
    public GameObject[] enemyPrefab; 
    public GameObject restartButton;
    public GameObject restartMenu;  
    public AudioSource gmAudio;
    public AudioClip zombie;
    public Transform[] spawnPoints;
    public TextMeshProUGUI soulEnergyText;
    public TextMeshProUGUI soulEnergyCollectedText;
    public TextMeshProUGUI currentRecordText;
    int randomSpawnPoint, randomEnemies;
    private int score;
    public int currentRecord;
    public bool isSomeoneDead = false;
    public bool isTaunterChased;
    public bool isShooterChased;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (score > PlayerPrefs.GetFloat("Current Record: ", 0))
        {
            currentRecordText.text = "Current Record: " + score;
        }
    }

    void StartGame()
    {
        InvokeRepeating(nameof(EnemySpawn), startDelay, spawnRate);
        isTaunterChased = false;
        isShooterChased = true;
    }
    void EnemySpawn()
    {
        randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        randomEnemies = UnityEngine.Random.Range(0, enemyPrefab.Length);
        Instantiate(enemyPrefab[randomEnemies], spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
    } 
    
    public void GameOver()
    {
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

}


