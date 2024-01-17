using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Boss_Brain_2ndPhase : MonoBehaviour
{
    //currentbugs: cannons stay for next attack. at rotation they don't shoot
    public HealthBar healthBar;
    private SpawnManager spawnManager;
    private GameManager gameManager;
    public Transform boss;
    bool phaseActive;
    //SPITTER HEADS
    [HideInInspector]
    public GameObject spitterHead;
    [HideInInspector]
    public Transform[] sHeadSpawnPointsRight;
    [HideInInspector]
    public Transform[] sHeadSpawnPointsLeft;
    //LASERS AND ZOMBIES
    public GameObject[] cannons;
    public GameObject zombie;
    public Transform[] cannonPoints;
    public Transform[] zombieSpawnPoints;
    int randomSpawnPoint;
    //SPAWNING EYEBALLS
    public GameObject eyeBomb;
    public Transform[] eyeSpawnPoints;
    //we can use zombieSpawnPoints;
    public bool lasersAndZombies = false;
    public bool eyeBombs = false;
    // Start is called before the first frame update
    void Start()
    {
        phaseActive = true;
        
        foreach (GameObject cannon in cannons)
        {
            // Instantiate the object at the current spawn point's position and rotation
            
            cannon.SetActive(false);
            //Instantiate(cannon, cannonPoints.position, cannonPoints.rotation);
        }

        StartCoroutine(AttackPatternRotation());
    }
    // Update is called once per frame
    void Update()
    {
        if (healthBar.currentHealth <= 0)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.bossDied = true;
            Destroy(gameObject);
            foreach (GameObject cannon in cannons)
            {
                // Instantiate the object at the current spawn point's position and rotation

                cannon.SetActive(false);
                //Instantiate(cannon, cannonPoints.position, cannonPoints.rotation);
            }
        }
        if (lasersAndZombies == false)
        {
            Debug.Log("Laser and zombies is false!");
            foreach (GameObject cannon in cannons)
            {
                Debug.Log("For each begun");
                BrainCannon cannonScript = cannon.GetComponent<BrainCannon>();
                // Instantiate the object at the current spawn point's position and rotation
                cannon.SetActive(false);
                //Instantiate(cannon, cannonPoints.position, cannonPoints.rotation);
            }
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
        //pain in the ass, use a single laser
        if (lasersAndZombies == true)
        {
            foreach (GameObject cannon in cannons)
            {
                // Instantiate the object at the current spawn point's position and rotation
                cannon.SetActive(true);
                BrainCannon cannonScript = cannon.GetComponent<BrainCannon>();
                StartCoroutine(cannonScript.SpawnLasers());
                //Instantiate(cannon, cannonPoints.position, cannonPoints.rotation);
            }
            //lasersAndZombies = true;
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
            lasersAndZombies = true;
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
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);
        foreach (GameObject laser in lasers)
            Destroy(laser);
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Boss hit");
            healthBar.currentHealth--;
            healthBar.UpdateHealthBar();
        }
    }
}
