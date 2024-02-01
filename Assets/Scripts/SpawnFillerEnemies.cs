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
        yield return new WaitForSeconds(3f);

        //GAME FREEZES AFTER THIS 3 SECONDS. THIS COROUTINE IS FIXED

        while (currentWaveIndex < waves.Length) //while the waveIndex is smaller then the lenght we set in the inspector, this repeats
        {
            //declaring a Wave variable that checks for the current index we have
            Wave currentWave = waves[currentWaveIndex];

            if (currentWave.enemies != null && currentWave.enemies.Length > 0)
            {
                Debug.Log("There are filler enemies in the list.");
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
            }
            else
            {
                // If there are no ranged enemies in the current wave, simply yield null to wait until the wave is complete
                yield return null;
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

            if (currentWave.specEnemies != null && currentWave.specEnemies.Length > 0)
            {
                Debug.Log("There are special enemies in the list.");
                // Continue spawning enemies until the score reaches the threshold
                while (currentScore < currentWave.scoreThreshold && !spawnManager.enemyLimitReached)
                {
                    // Randomly select an enemy prefab -> return an enemy from the currentwave's enemies list
                    SpecEnemySpawnInfo selectedEnemy = SelectSpecialEnemy(currentWave.specEnemies);

                    // Get a random spawn point for the current enemy
                    int randomSpawnPoint = UnityEngine.Random.Range(0, specSpawnPoints.Length);

                    GameObject specialEnemy = Instantiate(selectedEnemy.specEnemyPrefab, specSpawnPoints[randomSpawnPoint].position, Quaternion.identity);
                    spawnManager.AddEnemyToList(specialEnemy);    
                }
            }
            else
            {
                yield return null;
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

    // Inside your IsSpawnPointOccupied function
    bool IsSpawnPointOccupied(Transform spawnPoint)
    {
        // Check if the spawn point is in the list of occupied spawn points
        //A.K.A. checks if the occupiedSpawnPoints array returns the randomly picked spawnpint found in the SpawnRanged Coroutine
        return occupiedSpawnPoints.Contains(spawnPoint);
    }
    public IEnumerator SpawnRanged()
    {
        while (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];

            // Check if there are ranged enemies in the current wave
            if (currentWave.rangedEnemies != null && currentWave.rangedEnemies.Length > 0)
            {
                Debug.Log("There are ranged enemies in the list.");
                while (currentScore < currentWave.scoreThreshold && !spawnManager.enemyLimitReached)
                {
                    Debug.Log("Getting infos for spawning ranged enemies");

                    RangedEnemySpawnInfo selectedEnemy = SelectRangedEnemy(currentWave.rangedEnemies);

                    int randomSpawnPointIndex = UnityEngine.Random.Range(0, spawnPoints.Length);

                    // Check if the spawn point is occupied
                    bool isSpawnPointOccupied = IsSpawnPointOccupied(spawnPoints[randomSpawnPointIndex]);

                    if (!isSpawnPointOccupied)
                    {
                        Debug.Log("Spawn point available! Time to spawn");
                        GameObject rangedEnemy = Instantiate(selectedEnemy.rangedEnemyPrefab, spawnPoints[randomSpawnPointIndex].position, Quaternion.identity);
                        spawnManager.AddEnemyToList(rangedEnemy);

                        // Add the spawn point to the list of occupied spawn points
                        occupiedSpawnPoints.Add(spawnPoints[randomSpawnPointIndex]);

                        // Use the spawn rate of the selected enemy for the delay 
                    }
                    else
                    {
                        Debug.Log("Point occupied, looking for a new spawn point");
                        // If the spawn point is occupied, no need to choose another random spawn point here
                    }
                }
            }
            else
            {
                // If there are no ranged enemies in the current wave, simply yield null to wait until the wave is complete
                yield return null;
            }

            if (currentScore >= currentWave.scoreThreshold)
            {
                Debug.Log("Score Threshold reached!");
                occupiedSpawnPoints.Clear();
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