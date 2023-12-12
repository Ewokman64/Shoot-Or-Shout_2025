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
    //SPAWNING EYEBALLS
    public GameObject eyeBomb;
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
        foreach (Transform spawnPoints in zombieSpawnPoints)
        {
            // Instantiate the object at the current spawn point's position and rotation
            Instantiate(zombie, spawnPoints.position, spawnPoints.rotation);
        }
        yield return null;
    }
    public IEnumerator EyeBombs()
    {
        yield return null;
    }
    public IEnumerator FinalPush()
    {
        yield return null;
    }
}
