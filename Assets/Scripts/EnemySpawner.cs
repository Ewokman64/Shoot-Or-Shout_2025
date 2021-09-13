using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spawnedEnemy;

    public void Spawn(GameObject spitterPrefab)
    {
        spawnedEnemy = Instantiate(spitterPrefab, transform.position, Quaternion.identity);
    }
}
