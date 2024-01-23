using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves_Dungeon : MonoBehaviour
{
    SpawnManager spawnManager;

    public bool wave1Started = false;
    public bool wave2Started = false;
    public bool wave3Started = false;
    public bool wave4Started = false;
    public bool wave5Started = false;
    public bool wave6Started = false;      
    public bool wave7Started = false;
    public bool wave8Started = false;
    public bool miniBossSpawned = false;
    public bool bossSpawned = false;
    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    public void Wave1()
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        wave1Started = true;
}
    public void Wave2() 
    { 
        //StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.SpitterSpawn());
    }
    public void Wave3() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.EyeBombSpawn());
        wave3Started = true;
    }
    public void Wave4() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.BigBoiSpawn());
        wave4Started = true;
    }
    public void Wave5() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.SpitterSpawn());
        StartCoroutine(spawnManager.EyeBombSpawn());
        wave5Started = true;
    }
    public void Wave6() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.BigBoiSpawn());
        StartCoroutine(spawnManager.SpitterSpawn());
        wave6Started = true;
    }
    public void Wave7() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.BigBoiSpawn());
        wave7Started = true;
    }
    public void Wave8() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.SpitterSpawn());
        StartCoroutine(spawnManager.EyeBombSpawn());
        StartCoroutine(spawnManager.BigBoiSpawn());
        wave8Started = true;
    }
    public void MiniBossWave() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.NightKnightSpawn());
        miniBossSpawned = true;
    }
    public void BossWave() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.SpawnBrainBoss());
        bossSpawned = true;
    }
    public IEnumerator StopEnemySpawners()
    {
        StopCoroutine(spawnManager.ZombieSpawn());
        StopCoroutine(spawnManager.SpitterSpawn());
        StopCoroutine(spawnManager.EyeBombSpawn());
        StopCoroutine(spawnManager.BigBoiSpawn());
        yield return new WaitForSeconds(3);
    }
}
