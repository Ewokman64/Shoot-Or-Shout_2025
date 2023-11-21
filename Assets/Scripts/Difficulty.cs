using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Difficulty : MonoBehaviour
{
    private GameManager gameManager;
    private SpawnManager spawnManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    public void StartGame()
    {
        gameManager.StartGame();
        spawnManager.StartSpawnManager();
    }
}
