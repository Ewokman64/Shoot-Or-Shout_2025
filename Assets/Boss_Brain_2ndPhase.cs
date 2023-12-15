using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Brain_2ndPhase : MonoBehaviour
{
    public BrainBossHealthBar healthBar;
    bool phaseActive;
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
    public GameObject finalPush_zombie;
    public GameObject finalPush_spitter;
    public GameObject finalPush_eyeBomb;
    public GameObject finalPush_bigBoi;
    public GameObject finalPush_nightKnight;

    public bool lasersAndZombies = false;
    public bool eyeBombs = false;
    // Start is called before the first frame update
    void Start()
    {
        phaseActive = true;
        StartCoroutine(AttackPatternRotation());
    }
    // Update is called once per frame
    void Update()
    {
        if (healthBar.currentHealth <= 0)
        {
            FinalPush();
        }
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
        lasersAndZombies = true;
        foreach (Transform cannonPoints in cannonPoints)
        {
            // Instantiate the object at the current spawn point's position and rotation
            Instantiate(cannon, cannonPoints.position, cannonPoints.rotation);
        }
        while (lasersAndZombies == true)
        {
            lasersAndZombies = true;
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
        eyeBombs = true;
        while (eyeBombs == true)
        {
            foreach (Transform spawnPoints in eyeSpawnPoints)
            {
                randomSpawnPoint = UnityEngine.Random.Range(0, eyeSpawnPoints.Length);
                // Instantiate the object at the current spawn point's position and rotation
                Instantiate(eyeBomb, eyeSpawnPoints[randomSpawnPoint].position, spawnPoints.rotation);
                yield return new WaitForSeconds(2f);
            }
        }
    }
    public IEnumerator AttackPatternRotation()
    {
        //Wait X seconds, stop every coroutine, start next coroutine
        while (phaseActive)
        {
            // Start the next coroutine
            StartCoroutine(SpitterHeads());
            yield return new WaitForSeconds(5);

            // Stop the current coroutine
            StopCoroutine("SpitterHeads");
            ClearMap();
            yield return new WaitForSeconds(2);

            // Start the next coroutine
            StartCoroutine(LasersAndZombies());
            yield return new WaitForSeconds(5);

            // Stop the current coroutine
            StopCoroutine("LasersAndZombies");
            lasersAndZombies = false;
            ClearMap();
            yield return new WaitForSeconds(2);

            // Start the next coroutine
            StartCoroutine(EyeBombs());
            yield return new WaitForSeconds(5);

            // Stop the current coroutine
            StopCoroutine("EyeBombs");
            eyeBombs = false;
            ClearMap();
            yield return new WaitForSeconds(2);
        }
    }

    public void ClearMap()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
            Destroy(enemy);
    }
    public IEnumerator FinalPush()
    {
        //Boss turning into each enemy for a very short time
        //turns into buffed zombie
        Instantiate(finalPush_zombie, finalPush_zombie.transform.position, UnityEngine.Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(5);
        Destroy(finalPush_zombie);
        //turns into buffed spitter
        Instantiate(finalPush_spitter, finalPush_spitter.transform.position, UnityEngine.Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(5);
        Destroy(finalPush_spitter);
        //turns into buffed eyebomb
        Instantiate(finalPush_eyeBomb, finalPush_eyeBomb.transform.position, UnityEngine.Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(5);
        Destroy(finalPush_eyeBomb);
        //turns into buffed bigboi
        Instantiate(finalPush_bigBoi, finalPush_bigBoi.transform.position, UnityEngine.Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(5);
        Destroy(finalPush_bigBoi);
        //turns into buffed nightknight
        Instantiate(finalPush_nightKnight, finalPush_nightKnight.transform.position, UnityEngine.Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(5);
        Destroy(finalPush_nightKnight);
        //fokhen dies, fireworks and all
    }
}
