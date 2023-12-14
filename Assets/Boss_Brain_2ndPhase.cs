using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Brain_2ndPhase : MonoBehaviour
{
    //SPITTER HEADS
    [HideInInspector]
    public GameObject spitterHead;
    [HideInInspector]
    public Transform[] sHeadSpawnPointsRight;
    [HideInInspector]
    public Transform[] sHeadSpawnPointsLeft;
    //LASERS AND ZOMBIES
    public GameObject cannon;
    public GameObject zombie;
    public Transform[] cannonPoints;
    public Transform[] zombieSpawnPoints;
    int randomSpawnPoint;
    //SPAWNING EYEBALLS
    public GameObject eyeBomb;
    public Transform[] eyeSpawnPoints;
    //we can use zombieSpawnPoints;

    //TRANSFORMING INTO STRONGER ZOMBIES - THE FINAL PUSH
    public GameObject zombieV2;
    public GameObject spitterV2;
    public GameObject eyeBombV2;
    public GameObject bigBoiV2;
    public GameObject nightKnightV2;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpitterHeads());
        StartCoroutine(LasersAndZombies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpitterHeads()
    {
        yield return new WaitForSeconds(1);
        // Loop through each spawn point in the array
        foreach (Transform spawnPointRight in sHeadSpawnPointsRight)
        {
            // Instantiate the object at the current spawn point's position and rotation
            Instantiate(spitterHead, spawnPointRight.position, spawnPointRight.rotation);
        }
        foreach (Transform spawnPointLeft in sHeadSpawnPointsLeft)
        {
            // Instantiate the object at the current spawn point's position and rotation
            Instantiate(spitterHead, spawnPointLeft.position, spawnPointLeft.rotation);
        }
    }
    public IEnumerator LasersAndZombies()
    {
        foreach (Transform cannonPoints in cannonPoints)
        {
            // Instantiate the object at the current spawn point's position and rotation
            Instantiate(cannon, cannonPoints.position, cannonPoints.rotation);
        }
        while (true)
        {
            foreach (Transform spawnPoints in zombieSpawnPoints)
            {
                randomSpawnPoint = UnityEngine.Random.Range(0, zombieSpawnPoints.Length);
                // Instantiate the object at the current spawn point's position and rotation
                Instantiate(zombie, zombieSpawnPoints[randomSpawnPoint].position, spawnPoints.rotation);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator EyeBombs()
    {
        yield return new WaitForSeconds(2);
        while (true)
        {
            foreach (Transform spawnPoints in eyeSpawnPoints)
            {
                randomSpawnPoint = UnityEngine.Random.Range(0, eyeSpawnPoints.Length);
                // Instantiate the object at the current spawn point's position and rotation
                Instantiate(eyeBomb, eyeSpawnPoints[randomSpawnPoint].position, spawnPoints.rotation);
            }
            yield return new WaitForSeconds(1f);
        }
    }
    public IEnumerator FinalPush()
    {
        yield return null;
    }
}
