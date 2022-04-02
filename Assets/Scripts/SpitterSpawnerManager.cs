using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpitterSpawnerManager : MonoBehaviour
{
    private GameManager gameManager;
    public GameObject spitterPrefab;
    public GameObject cannonYetiPrefab;
    public EnemySpawner[] enemySpawnerArray;
    [HideInInspector]
    public float spitter_startDelay = 5;
    [HideInInspector]
    public float spitter_spawnRate = 3;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartSpitterSpawn()
    {
        enemySpawnerArray = GetComponentsInChildren<EnemySpawner>();
        StartCoroutine(TrySpawnSpitter());
        //InvokeRepeating(nameof(TrySpawnSpitter), spitter_StartDelay, spitter_SpawnRate);
    }
    // Update is called once per frame
    void Update()
    {        
        if (gameManager.isSomeoneDead == true)
        {
            gameManager.GameOver();
            StopCoroutine("TrySpawnSpitter");
        }
    }
    object[] Shuffle(object[] array)
    {
        System.Random random = new System.Random();
        for (int i = array.Length - 1; i > 0; i--)
        {
            int swapIndex = random.Next(i + 1);
            object temp = array[i];
            array[i] = array[swapIndex];
            array[swapIndex] = temp;
        }
        return array;
    }
    /*public void TrySpawnSpitter()
    {
        EnemySpawner spawner = (((EnemySpawner[])Shuffle(enemySpawnerArray)).FirstOrDefault(enemySpawner => enemySpawner.spawnedEnemy == null));
        if (spawner != null)
        {
            spawner.Spawn(spitterPrefab);
        }
    }*/
    IEnumerator TrySpawnSpitter()
    {
        yield return new WaitForSeconds(spitter_startDelay);
        EnemySpawner spawner = (((EnemySpawner[])Shuffle(enemySpawnerArray)).FirstOrDefault(enemySpawner => enemySpawner.spawnedEnemy == null));
        if (spawner != null)
        {
            spawner.Spawn(spitterPrefab);
        }
        yield return new WaitForSeconds(spitter_spawnRate);
    }
}
