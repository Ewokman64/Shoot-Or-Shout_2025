using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private GameManager gameManager;
    private DimensionManager dimensionManager;
    //----------------------------------------
    public GameObject[] enemyPrefab;
    public GameObject powerUpPrefab;
    public Transform[] spawnPoints;
    private float startDelay = 2;
    public float spawnRate;

    int randomSpawnPoint, randomEnemies;

    public Transform[] powerUpSpawnPoints;

    [HideInInspector]
    public float powerUp_startDelay = 10;
    [HideInInspector]
    public float powerUp_spawnRate = 10;

    public int powerUps;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        dimensionManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
    }

    public void StartSpawnManager()
    {
        StartCoroutine(ZombieSpawn());
        StartCoroutine(PowerUpSpawn());
        powerUps = 0;
    }
    // Update is called once per frame
    void Update()
    {
        /*if (gameManager.isSomeoneDead == true)
        {
            gameManager.GameOver();
            StopCoroutine("ZombieSpawn");
            StopCoroutine("PowerUpSpawn");
            StopCoroutine("TrySpawnSpitter");
        }
        if (dimensionManager.IceDimension.activeSelf)
        {
            StopCoroutine("ZombieSpawn");
            StopCoroutine("TrySpawnSpitter");
        }*/
    }
    IEnumerator ZombieSpawn()
    {
        yield return new WaitForSeconds(startDelay);
        while (dimensionManager.DungeonDimension.activeSelf)
        {
            randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
            randomEnemies = UnityEngine.Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomEnemies], spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(spawnRate);
        }       
    }
    IEnumerator PowerUpSpawn()
    {       
        while (powerUps == 0)
        {
            yield return new WaitForSeconds(powerUp_startDelay);

            randomSpawnPoint = UnityEngine.Random.Range(0, powerUpSpawnPoints.Length);
            Instantiate(powerUpPrefab, powerUpSpawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
            powerUps = 1;
            yield return new WaitForSeconds(powerUp_spawnRate);
        }      
    }
}
