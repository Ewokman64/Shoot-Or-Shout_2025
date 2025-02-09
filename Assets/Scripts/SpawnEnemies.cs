using System.Collections;
using System.Collections.Generic;
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
public class SpawnEnemies : MonoBehaviour
{
    public Wave[] waves;
    public int currentWaveIndex = 0;

    private float spawnRate = 0.6f;
    private float specSpawnRate = 5f;
    private float rangedSpawnRate = 3;

    //SpawnPoints of filler and ranged enemies
    [HideInInspector]
    public Transform[] spawnPoints;
    //SpawnPoints of Special enemies
    [HideInInspector]
    public Transform[] specSpawnPoints;

    // Declare a List to keep track of occupied spawn points
    [HideInInspector]
    public List<Transform> occupiedSpawnPoints = new List<Transform>();

    //public bool spawnFillerRunning = false;
    private int currentScore = 0;

    private GameManager gameManager;
    private SpawnManager spawnManager;
    //private UpgradesManager upgradesManager;
    public PowerUpDisplay powerUpDisplay;
    private PowerupList upgradeList;

    private bool enemyLimitReached = false;
    public int enemyCount;
    public int enemyLimit;
    public List<GameObject> enemies = new List<GameObject>();
    //declaring a Wave variable that checks for the current index we have
    public Wave currentWave;

    public bool wavesDefeated = false;
    public GameObject miniBoss;
    public GameObject boss;
    public Transform bossSpawnPoint;
    public int miniBossScoreThreshold = 350;
    public bool miniBossSpawned = false;
    bool miniBossReady = false;
    bool bossReady;
    public bool bossSpawned = false;
    public NightKnight nightKnightScript;
    public Horse horseScript;
    public bool nightKnightDead = false;
    public bool horseDead = false;
    public void Start()
    {
        enemyLimit = 15;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        //upgradesManager = GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
        upgradeList = GameObject.Find("PowerupManager").GetComponent<PowerupList>();
        powerUpDisplay = GameObject.Find("PowerupManager").GetComponent<PowerUpDisplay>();
        nightKnightScript = miniBoss.GetComponent<NightKnight>();
        horseScript = miniBoss.GetComponentInChildren<Horse>();
    }
    public void CheckCurrentWave()
    {
        //assign the current index
        currentWave = waves[currentWaveIndex];

        //if we reach the curent wave's score, we offe the upgrades and clear the map, then adding the next waveindex (a.k.a. next wave incoming)
        if (currentScore >= currentWave.scoreThreshold)
        {
            Debug.Log("Score Threshold reached!");
            occupiedSpawnPoints.Clear();
            gameManager.ClearMap();
            powerUpDisplay.OfferPowerUps();
            currentWaveIndex++;
        }
    }
    private void Update()
    {
        currentScore = gameManager.score;
        // Check if the score has reached the threshold
        if (currentScore >= miniBossScoreThreshold && miniBossSpawned == false)
        {
            // Spawn the miniboss
            StartCoroutine(SpawnMiniBoss());
            if (miniBossReady)
            {
                Instantiate(miniBoss, bossSpawnPoint.transform.position, Quaternion.identity);
                miniBossSpawned = true;
            }
        }     
        if (nightKnightDead && horseDead && !bossSpawned)
        {
            StartCoroutine(SpawnBoss());
            if (bossReady)
            {
                Instantiate(boss, bossSpawnPoint.transform.position, Quaternion.identity);
                bossSpawned = true;
            }     
        }
        UpdateEnemyList();
    }
    public IEnumerator SpawnMiniBoss()
    {
        yield return new WaitForSeconds(3);
        miniBossReady = true;   
    }
    public IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(3);
        bossReady = true;
    }
    public void StartWaves()
    {
        StartCoroutine(SpawnFiller());
        StartCoroutine(SpawnSpecials());
        StartCoroutine(SpawnRanged());
    }
    public IEnumerator SpawnFiller()
    {
        Debug.Log("Spawn Started");
        yield return new WaitForSeconds(3f);

        while (currentWaveIndex < waves.Length) //while the waveIndex is smaller than the lenght we set in the inspector, this repeats
        {

            CheckCurrentWave();

            //checks if there are enemies in the list, a.k.a if the current wave contains filler enemies or not
            if (currentWave.enemies != null && currentWave.enemies.Length > 0)
            {  
                if (!enemyLimitReached)
                {   
                    while (currentScore < currentWave.scoreThreshold && !enemyLimitReached)
                    {
                        // Randomly select an enemy prefab -> return an enemy from the currentwave's enemies list
                        EnemySpawnInfo selectedEnemy = SelectFillerEnemy(currentWave.enemies);
                        // Get a random spawn point for the current enemy
                        int randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
                        GameObject enemy = Instantiate(selectedEnemy.enemyPrefab, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
                        enemies.Add(enemy);
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
                // If there are no filler enemies in the current wave, simply yield null to wait until the wave is complete
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

    public IEnumerator SpawnSpecials()
    {
        yield return new WaitForSeconds(3f);

        while (currentWaveIndex < waves.Length) //while the waveIndex is smaller then the lenght we set in the inspector, this repeats
        {

            CheckCurrentWave();

            if (currentWave.specEnemies != null && currentWave.specEnemies.Length > 0)
            {
                Debug.Log("There are special enemies in the list.");
                // Continue spawning enemies until the score reaches the threshold
                if (!enemyLimitReached)
                {
                    while (currentScore < currentWave.scoreThreshold)
                    {
                        // Randomly select an enemy prefab -> return an enemy from the currentwave's enemies list
                        SpecEnemySpawnInfo selectedEnemy = SelectSpecialEnemy(currentWave.specEnemies);
                        // Get a random spawn point for the current enemy
                        int randomSpawnPoint = UnityEngine.Random.Range(0, specSpawnPoints.Length);
                        GameObject specialEnemy = Instantiate(selectedEnemy.specEnemyPrefab, specSpawnPoints[randomSpawnPoint].position, Quaternion.identity);
                        enemies.Add(specialEnemy);
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
                if (!enemyLimitReached)
                {
                    while (currentScore < currentWave.scoreThreshold && !enemyLimitReached)
                    {
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
                                GameObject rangedEnemy = Instantiate(selectedEnemy.rangedEnemyPrefab, selectedSpawnPoint.position, Quaternion.identity);
                                //gameManager.AddEnemyToList(rangedEnemy);
                                enemies.Add(rangedEnemy);
                                // Associate the spawned enemy with its spawn point
                                rangedEnemy.GetComponent<RangedEnemy>().associatedSpawnPoint = selectedSpawnPoint;

                                // Add the spawn point to the list of occupied spawn points
                                occupiedSpawnPoints.Add(selectedSpawnPoint);

                                spawned = true; // Set spawned to true to break out of the loop
                            }
                            else
                            {
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
            // Randomly select an enemy prefab and return its spawn rate and spawn points
            return rangedEnemies[Random.Range(0, rangedEnemies.Length)];
        }
        else
        {
            return null;
        }
    }

    public void UpdateEnemyList()
    {
        if (enemies.Count >= enemyLimit)
        {
            enemyLimitReached = true;
        }
        else
        {
            enemyLimitReached = false;
        }
        for (int i = 0; i < enemies.Count; i++)
        {
            // Check if the enemy GameObject is null (destroyed)
            if (enemies[i] == null)
            {
                // Optionally, you can remove the destroyed enemy from the list
                enemies.RemoveAt(i);
            }
        }
    }
}