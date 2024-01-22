using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves_Dungeon : MonoBehaviour
{
    SpawnManager spawnManager;
    private void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    public void Wave1()
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
    }
    public void Wave2() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.SpitterSpawn());
    }
    public void Wave3() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.EyeBombSpawn());
    }
    public void Wave4() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.BigBoiSpawn());
    }
    public void Wave5() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.SpitterSpawn());
        StartCoroutine(spawnManager.EyeBombSpawn());
    }
    public void Wave6() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.BigBoiSpawn());
        StartCoroutine(spawnManager.SpitterSpawn());
    }
    public void Wave7() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.BigBoiSpawn());
    }
    public void Wave8() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.ZombieSpawn());
        StartCoroutine(spawnManager.SpitterSpawn());
        StartCoroutine(spawnManager.EyeBombSpawn());
        StartCoroutine(spawnManager.BigBoiSpawn());
    }
    public void MiniBossWave() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.NightKnightSpawn());
    }
    public void BossWave() 
    {
        StopEnemySpawners();
        StartCoroutine(spawnManager.SpawnBrainBoss());    
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
