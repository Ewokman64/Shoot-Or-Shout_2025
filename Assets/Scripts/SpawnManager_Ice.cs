using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager_Ice : MonoBehaviour
{
    private DimensionManager dimensionManager;
    public GameObject[] yetiPrefab;
    int randomSpawnPoint, randomYeties;
    public Transform[] spawnPoints;
    public float startDelay = 5;
    public float spawnRate = 5;
    // Start is called before the first frame update
    void Start()
    {
        dimensionManager = GameObject.Find("DimensionManager").GetComponent<DimensionManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dimensionManager.IceDimension.activeSelf)
        {
            StartCoroutine(YetiSpawn());
        }       
    }
    IEnumerator YetiSpawn()
    {
        randomSpawnPoint = UnityEngine.Random.Range(0, spawnPoints.Length);
        randomYeties = UnityEngine.Random.Range(0, yetiPrefab.Length);
        Instantiate(yetiPrefab[randomYeties], spawnPoints[randomSpawnPoint].position, UnityEngine.Quaternion.identity);
        yield return new WaitForSeconds(spawnRate);
    }
}
