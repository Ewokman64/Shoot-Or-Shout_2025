using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public EnemySpawnInfo[] enemies; //we made a class below, containing newly declared stuff. then we reference that class here and call it enemies
    public SpecEnemySpawnInfo[] specEnemies;
    public RangedEnemySpawnInfo[] rangedEnemies;
    public int scoreThreshold; // Score at which the wave is considered complete
}

[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;

    public EnemySpawnInfo(GameObject prefab)
    {
        enemyPrefab = prefab;
    }
}

[System.Serializable]

public class SpecEnemySpawnInfo
{
    public GameObject specEnemyPrefab;

    public SpecEnemySpawnInfo(GameObject specPrefab)
    {
        specEnemyPrefab = specPrefab;
    }
}

[System.Serializable]
public class RangedEnemySpawnInfo
{
    public GameObject rangedEnemyPrefab;

    public RangedEnemySpawnInfo(GameObject rangedPrefab)
    {
        rangedEnemyPrefab = rangedPrefab;
    }
}
public class SpawnFillerEnemies : MonoBehaviour
{
    public Wave[] waves;
    public int currentWaveIndex = 0;

    private float spawnRate = 0.6f;
    private float specSpawnRate = 5f;

    public Transform[] spawnPoints;
    public Transform[] specSpawnPoints;
    //FILLER AND RANGED can use same spawnpoints
    public EnemySpawner[] rangedSpawners;

    public bool spawnFillerRunning = false;
    private int currentScore = 0;

    public GameManager gameManager;
    public SpawnManager spawnManager;
    public UpgradesManager upgradesManager;
    public UpgradeList upgradeList;


    void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        upgradesManager = GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
        upgradeList = GameObject.Find("UpgradesManager").GetComponent<UpgradeList>();
    }
    private void Update()
    {
        currentScore = gameManager.score;
    }
    public IEnumerator SpawnWaves()
    { 
        while (currentWaveIndex < waves.Length) //while the waveIndex is smaller then the lenght we set in the inspector, this repeats
        {
            //declaring a Wave variable that checks for the current index we have
            Wave currentWave = waves[currentWaveIndex];
            // Continue spawning enemies until the score reaches the threshold
            while (currentScore < currentWave.scoreThreshold && !spawnManager.enemyLimitReached)
            {
                  // Randomly select an enemy prefab -> return an enemy from the currentwave's enemies list
                  EnemySpawnInfo selectedEnemy = SelectFillerEnemy(currentWave.enemies);

                  // Get a random spawn point for the current enemy
                  int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);

                  GameObject enemy = Instantiate(selectedEnemy.enemyPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                  spawnManager.AddEnemyToList(enemy);

                  // Use the spawn rate of the selected enemy for the delay
                  yield return new WaitForSeconds(spawnRate);
            }
            if (currentScore >= currentWave.scoreThreshold)
            {
                Debug.Log("Score Treshold reached!");
                gameManager.ClearMap();
                upgradesManager.OfferUpgrades();
                currentWaveIndex++;
            }
            else
            {
                yield return null;
            }
        }
    }
    EnemySpawnInfo SelectFillerEnemy(EnemySpawnInfo[] enemies) //we grab the enemies list from our class called EnemySpawnInfo (named it SelectRandomEnemy)
    {
        if(enemies != null)
        {
            // Randomly select an enemy prefab and return its spawn rate and spawn points
            return enemies[Random.Range(0, enemies.Length)];
        }
        else
        {
            return null;
        }    
    }

    public IEnumerator SpawnSpecialWaves()
    {
        while (currentWaveIndex < waves.Length) //while the waveIndex is smaller then the lenght we set in the inspector, this repeats
        {
            //declaring a Wave variable that checks for the current index we have
            Wave currentWave = waves[currentWaveIndex];

            // Continue spawning enemies until the score reaches the threshold
            while (currentScore < currentWave.scoreThreshold && !spawnManager.enemyLimitReached)
            {  
                // Randomly select an enemy prefab -> return an enemy from the currentwave's enemies list
                SpecEnemySpawnInfo selectedEnemy = SelectSpecialEnemy(currentWave.specEnemies);

                // Get a random spawn point for the current enemy
                int randomSpawnPoint = UnityEngine.Random.Range(0, specSpawnPoints.Length);

                GameObject specialEnemy = Instantiate(selectedEnemy.specEnemyPrefab, specSpawnPoints[randomSpawnPoint].position, Quaternion.identity);
                spawnManager.AddEnemyToList(specialEnemy);

                // Use the spawn rate of the selected enemy for the delay
                yield return new WaitForSeconds(specSpawnRate);
            }
            if (currentScore >= currentWave.scoreThreshold)
            {
                Debug.Log("Score Treshold reached!");
                gameManager.ClearMap();
                upgradesManager.OfferUpgrades();
                currentWaveIndex++;
            }
            else
            {
                yield return null;
            }
        }
    }
    SpecEnemySpawnInfo SelectSpecialEnemy(SpecEnemySpawnInfo[] specEnemies) //we grab the enemies list from our class called EnemySpawnInfo (named it SelectRandomEnemy)
    {
        if (specEnemies != null)
        {
            // Randomly select an enemy prefab and return its spawn rate and spawn points
            return specEnemies[Random.Range(0, specEnemies.Length)];
        }
        else
        {
            return null;
        }
    }

    /*public IEnumerator SpawnRanged()
    {
        rangedSpawners = GetComponentsInChildren<EnemySpawner>();

        //looking for the first available spawnpoint that doesn't contain a spitter
        //EnemySpawner spawner = rangedSpawners.FirstOrDefault(enemySpawner => enemySpawner.spitter == null);

        

        while (currentWaveIndex < waves.Length) //while the waveIndex is smaller then the lenght we set in the inspector, this repeats
        {
            //declaring a Wave variable that checks for the current index we have
            Wave currentWave = waves[currentWaveIndex];

            // Randomly select an enemy prefab -> return an enemy from the currentwave's enemies list
            RangedEnemySpawnInfo selectedEnemy = SelectRangedEnemy(currentWave.rangedEnemies);

            Transform[] r_spawnpoints = rangedSpawners.FirstOrDefault(selectedEnemy => selectedEnemy.rangedPrefab == null);
            // Continue spawning enemies until the score reaches the threshold
            while (currentScore < currentWave.scoreThreshold && !spawnManager.enemyLimitReached && spawner != null)
            {
                // Get a random spawn point for the current enemy
                int randomSpawnPoint = UnityEngine.Random.Range(0, rangedSpawners.Length);

                GameObject rangedEnemy = Instantiate(selectedEnemy.rangedEnemyPrefab, rangedSpawners[randomSpawnPoint].transform.position, Quaternion.identity);
                spawnManager.AddEnemyToList(rangedEnemy);

                // Use the spawn rate of the selected enemy for the delay
                yield return new WaitForSeconds(specSpawnRate);
            }
            if (currentScore >= currentWave.scoreThreshold)
            {
                Debug.Log("Score Treshold reached!");
                gameManager.ClearMap();
                upgradesManager.OfferUpgrades();
                currentWaveIndex++;
            }
            else
            {
                yield return null;
            }
        }
    }
    RangedEnemySpawnInfo SelectRangedEnemy(RangedEnemySpawnInfo[] rangedEnemies) //we grab the enemies list from our class called EnemySpawnInfo (named it SelectRandomEnemy)
    {
        if (rangedEnemies != null)
        {
            // Randomly select an enemy prefab and return its spawn rate and spawn points
            return rangedEnemies[Random.Range(0, rangedEnemies.Length)];
        }
        else
        {
            return null;
        }
    }*/
}