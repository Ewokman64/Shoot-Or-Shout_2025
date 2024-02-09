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
    private float rangedSpawnRate = 3;

    //SpawnPoints of filler and ranged enemies
    public Transform[] spawnPoints;
    //SpawnPoints of Special enemies
    public Transform[] specSpawnPoints;

    // Declare a List to keep track of occupied spawn points
    public List<Transform> occupiedSpawnPoints = new List<Transform>();

    public bool spawnFillerRunning = false;
    private int currentScore = 0;

    public GameManager gameManager;
    public SpawnManager spawnManager;
    public UpgradesManager upgradesManager;
    public UpgradeList upgradeList;

    //declaring a Wave variable that checks for the current index we have
    public Wave currentWave;


    public void Start()
    {
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        upgradesManager = GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
        upgradeList = GameObject.Find("UpgradesManager").GetComponent<UpgradeList>();
    }
    public void CheckCurrentWave()
    {
        //assign the current index
        currentWave = waves[currentWaveIndex];
        if (currentScore >= currentWave.scoreThreshold)
        {
            Debug.Log("Score Threshold reached!");
            occupiedSpawnPoints.Clear();
            gameManager.ClearMap();
            upgradesManager.OfferUpgrades();
            currentWaveIndex++;
        }
    }
    private void Update()
    {
        currentScore = gameManager.score;
    }
    public IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(3f);

        //GAME FREEZES fairly randomly maybe once spawn limit reached again?

        while (currentWaveIndex < waves.Length) //while the waveIndex is smaller then the lenght we set in the inspector, this repeats
        {
            CheckCurrentWave();

            if (currentWave.enemies != null && currentWave.enemies.Length > 0)
            {
                Debug.Log("There are filler enemies in the list.");
                if (!spawnManager.enemyLimitReached)
                {
                    while (currentScore < currentWave.scoreThreshold && !spawnManager.enemyLimitReached)
                    {
                        // Randomly select an enemy prefab -> return an enemy from the currentwave's enemies list
                        EnemySpawnInfo selectedEnemy = SelectFillerEnemy(currentWave.enemies);

                        // Get a random spawn point for the current enemy
                        int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);

                        GameObject enemy = Instantiate(selectedEnemy.enemyPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                        gameManager.AddEnemyToList(enemy);
                        yield return new WaitForSeconds(spawnRate);
                    }
                }
                else
                {   
                    yield return null;
                }
            }
            else
            {
                // If there are no ranged enemies in the current wave, simply yield null to wait until the wave is complete
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
        yield return new WaitForSeconds(3f);

        while (currentWaveIndex < waves.Length) //while the waveIndex is smaller then the lenght we set in the inspector, this repeats
        {

            CheckCurrentWave();

            if (currentWave.specEnemies != null && currentWave.specEnemies.Length > 0)
            {
                Debug.Log("There are special enemies in the list.");
                // Continue spawning enemies until the score reaches the threshold
                if (!spawnManager.enemyLimitReached)
                {
                    while (currentScore < currentWave.scoreThreshold)
                    {
                        // Randomly select an enemy prefab -> return an enemy from the currentwave's enemies list
                        SpecEnemySpawnInfo selectedEnemy = SelectSpecialEnemy(currentWave.specEnemies);

                        // Get a random spawn point for the current enemy
                        int randomSpawnPoint = UnityEngine.Random.Range(0, specSpawnPoints.Length);

                        GameObject specialEnemy = Instantiate(selectedEnemy.specEnemyPrefab, specSpawnPoints[randomSpawnPoint].position, Quaternion.identity);
                        gameManager.AddEnemyToList(specialEnemy);

                        yield return new WaitForSeconds(specSpawnRate);
                    }
                }
                else
                {
                    
                    yield return null;
                }
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
    public IEnumerator SpawnRanged()
    {
        yield return new WaitForSeconds(3f);

        while (currentWaveIndex < waves.Length)
        {
            CheckCurrentWave();

            // Check if there are ranged enemies in the current wave
            if (currentWave.rangedEnemies != null && currentWave.rangedEnemies.Length > 0)
            {
                if (!spawnManager.enemyLimitReached)
                {
                    while (currentScore < currentWave.scoreThreshold && !spawnManager.enemyLimitReached)
                    {
                        Debug.Log("Getting infos for spawning ranged enemies");

                        RangedEnemySpawnInfo selectedEnemy = SelectRangedEnemy(currentWave.rangedEnemies);
                        bool spawned = false;
                        bool isThereSpace = true;

                        if (occupiedSpawnPoints.Count == 3)
                        {
                            isThereSpace = false;
                        }
                        
                        while (!spawned && isThereSpace)
                        {
                            int randomSpawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);
                            Transform selectedSpawnPoint = spawnPoints[randomSpawnPointIndex];

                            // Check if the spawn point is occupied
                            bool isSpawnPointOccupied = IsSpawnPointOccupied(selectedSpawnPoint);

                            if (!isSpawnPointOccupied)
                            {
                                Debug.Log("Spawn point available! Time to spawn");
                                GameObject rangedEnemy = Instantiate(selectedEnemy.rangedEnemyPrefab, selectedSpawnPoint.position, Quaternion.identity);
                                gameManager.AddEnemyToList(rangedEnemy);

                                // Associate the spawned enemy with its spawn point
                                rangedEnemy.GetComponent<RangedEnemy>().associatedSpawnPoint = selectedSpawnPoint;

                                // Add the spawn point to the list of occupied spawn points
                                occupiedSpawnPoints.Add(selectedSpawnPoint);

                                spawned = true; // Set spawned to true to break out of the loop
                            }
                            else
                            {
                                Debug.Log("Point occupied, looking for a new spawn point");
                                yield return new WaitForSeconds(0.1f);
                            }
                        }
                        yield return new WaitForSeconds(rangedSpawnRate);
                    }
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                // If there are no ranged enemies in the current wave, simply yield null to wait until the wave is complete
                yield return null;
            }
        }
    }
    bool IsSpawnPointOccupied(Transform spawnPoint)
    {
        return occupiedSpawnPoints.Contains(spawnPoint);
    }

    public void OnEnemyDestroyed(GameObject enemy)
    {
        RangedEnemy rangedEnemy = enemy.GetComponent<RangedEnemy>();
        if (rangedEnemy != null && rangedEnemy.associatedSpawnPoint != null)
        {
            occupiedSpawnPoints.Remove(rangedEnemy.associatedSpawnPoint);
        }
    }
    RangedEnemySpawnInfo SelectRangedEnemy(RangedEnemySpawnInfo[] rangedEnemies) //we grab the enemies list from our class called EnemySpawnInfo (named it SelectRandomEnemy)
    {
        if (rangedEnemies != null)
        {
            Debug.Log("Ranged Enemy Prefab Selected");
            // Randomly select an enemy prefab and return its spawn rate and spawn points
            return rangedEnemies[Random.Range(0, rangedEnemies.Length)];
        }
        else
        {
            return null;
        }
    }
}