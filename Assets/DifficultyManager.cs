using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    private SpitterSpawnerManager spitterSpawnerManager;
    public bool easyMode;
    public bool normalMode;
    public bool hardMode;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        spitterSpawnerManager = GameObject.Find("EnemySpawnerManager").GetComponent<SpitterSpawnerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EasyModeSettings()
    {
        spawnManager.spawnRate = 1;
        spitterSpawnerManager.spitter_StartDelay = 5;
        spitterSpawnerManager.spitter_SpawnRate = 5;
    }

    public void NormalModeSettings()
    {
        spawnManager.spawnRate = 0.5f;
        spitterSpawnerManager.spitter_StartDelay = 5;
        spitterSpawnerManager.spitter_SpawnRate = 3;
    }

    public void HardModeSettings()
    {
        spawnManager.spawnRate = 0.5f;
        spitterSpawnerManager.spitter_StartDelay = 0.5f;
        spitterSpawnerManager.spitter_SpawnRate = 0.5f;
    }
}
