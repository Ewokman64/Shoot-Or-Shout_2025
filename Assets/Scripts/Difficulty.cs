using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private SpitterSpawnerManager spitterSpawnerManager;
    private DifficultyManager difficultyManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        spitterSpawnerManager = GameObject.Find("EnemySpawnerManager").GetComponent<SpitterSpawnerManager>();
        difficultyManager = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
    }

    public void StartEasyMode()
    {
        difficultyManager.easyMode = true;
        gameManager.StartGame();
        spawnManager.StartSpawnManager();
        spitterSpawnerManager.StartSpitterSpawn();
    }
    public void StartNormalMode()
    {
        difficultyManager.normalMode = true;
        gameManager.StartGame();
        spawnManager.StartSpawnManager();
        spitterSpawnerManager.StartSpitterSpawn();
    }
    public void StartHardMode()
    {
        difficultyManager.hardMode = true;
        gameManager.StartGame();
        spawnManager.StartSpawnManager();
        spitterSpawnerManager.StartSpitterSpawn();
    }
}
