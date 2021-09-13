using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpitterSpawnerManager : MonoBehaviour
{
    public GameObject spitterPrefab;
    public EnemySpawner[] enemySpawnerArray;
    private float spitter_StartDelay = 5;
    private float spitter_SpawnRate = 3;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawnerArray = GetComponentsInChildren<EnemySpawner>();
        InvokeRepeating(nameof(TrySpawnSpitter), spitter_StartDelay, spitter_SpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
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
    public void TrySpawnSpitter()
    {
        EnemySpawner spawner = (((EnemySpawner[])Shuffle(enemySpawnerArray)).FirstOrDefault(enemySpawner => enemySpawner.spawnedEnemy == null));
        if (spawner != null)
        {
            spawner.Spawn(spitterPrefab);
        }
    }
}
