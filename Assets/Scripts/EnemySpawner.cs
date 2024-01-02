using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spitter;

    public void Spawn(GameObject spitterPrefab)
    {
        spitter = Instantiate(spitterPrefab, transform.position, Quaternion.identity);
    }
}
