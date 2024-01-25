using System.Collections;
using UnityEngine;

[System.Serializable]
public class Wave
{
    public EnemySpawnInfo[] enemies;
    public EnemySpawnInfo[] normalEnemies;
    //might be forced to add everyenemytype
    //OR BETTER. make the filler enemies frequent, use a single long CD for everything else?
    public int scoreThreshold; // Score at which the wave is considered complete
    // Add any other parameters you need for this wave
}
[System.Serializable]
public class EnemySpawnInfo
{
    public GameObject enemyPrefab;
    public GameObject normalEnemyPrefab;
    public float spawnRate;
    public Transform[] spawnPoints; // Array of spawn points for this enemy

    public EnemySpawnInfo(GameObject prefab, GameObject normalPrefab, float rate, Transform[] points)
    {
        enemyPrefab = prefab;
        normalEnemyPrefab = normalPrefab;
        spawnRate = rate;
        spawnPoints = points;
    }
}

public class SpawnManagerNew : MonoBehaviour
{
    public Wave[] waves;
    private int currentWaveIndex = 0;
    private float spawnRate = 3f;
    private float fillerSpawnRate = 0.5f;
    public bool spawnFillerRunning = false;
    // Placeholder for your scoring system; replace with your actual scoring logic
    private int currentScore = 0;
    public int mobCap = 0;
    public GameManager gameManager;
    public SpawnManager spawnManager;
    public UpgradesManager upgradesManager;
    public UpgradeList upgradeList;


    void Start()
    {
        //StartCoroutine(SpawnWave());
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        upgradesManager = GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
        upgradeList = GameObject.Find("UpgradesManager").GetComponent<UpgradeList>();
    }
    private void Update()
    {
        currentScore = gameManager.score;
    }
    public IEnumerator SpawnFiller()
    { 
        while (currentWaveIndex < waves.Length)
        {
            //ONCE THE CAP IS REACHED THE GAME CRASHES
            Wave currentWave = waves[currentWaveIndex];
            // Continue spawning enemies until the score reaches the threshold
            while (currentScore < currentWave.scoreThreshold)
            {
                if (!spawnManager.enemyLimitReached)
                {
                    EnemySpawnInfo selectedNEnemy = SelectRandomEnemy(currentWave.normalEnemies);
                    Vector2 spawnNPosition = GetRandomSpawnPosition(selectedNEnemy.spawnPoints);
                    GameObject fillerEnemy = Instantiate(selectedNEnemy.normalEnemyPrefab, spawnNPosition, Quaternion.identity);
                    spawnManager.AddEnemyToList(fillerEnemy);
                }
                yield return new WaitForSeconds(fillerSpawnRate);
                
            }
            if (currentScore >=  currentWave.scoreThreshold)
            {
                Debug.Log("Score Treshold reached!");
                gameManager.ClearMap();
                upgradesManager.OfferUpgrades();
                currentWaveIndex++;
            }
            yield return new WaitForSeconds(5);
            Debug.Log("Current Wave Index: " + currentWaveIndex);
            //WaveIndex grows without problem, but while loop doesn't start over
        }
    }
    public IEnumerator SpawnWaves()
    { 
        while (currentWaveIndex < waves.Length)
        {
            Wave currentWave = waves[currentWaveIndex];
            // Continue spawning enemies until the score reaches the threshold
            while (currentScore < currentWave.scoreThreshold && !spawnManager.enemyLimitReached)
            {
                  // Randomly select an enemy prefab
                  EnemySpawnInfo selectedEnemy = SelectRandomEnemy(currentWave.enemies);
                  // Get a random spawn point for the current enemy
                  Vector2 spawnPosition = GetRandomSpawnPosition(selectedEnemy.spawnPoints);
                  GameObject enemy = Instantiate(selectedEnemy.enemyPrefab, spawnPosition, Quaternion.identity);
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
        }
    }
    EnemySpawnInfo SelectRandomEnemy(EnemySpawnInfo[] enemies)
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

    Vector2 GetRandomSpawnPosition(Transform[] spawnPoints)
    {
        // Get a random index for the spawnPoints array
        int randomIndex = Random.Range(0, spawnPoints.Length);

        // Use the selected spawn point's position
        return spawnPoints[randomIndex].position;
    }
}