using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    private SpitterSpawnerManager spitterSpawnerManager;
    public bool normalMode;
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

    public void NormalModeSettings()
    {
        spawnManager.spawnRate = 0.5f;
        spitterSpawnerManager.spitter_startDelay = 5;
        spitterSpawnerManager.spitter_spawnRate = 3;
    }
}
