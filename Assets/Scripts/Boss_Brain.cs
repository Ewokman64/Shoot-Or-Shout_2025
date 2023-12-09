using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Brain : MonoBehaviour
{
    int health = 200;
    int speed = 5;
    bool brainIsAlive;
    //TENTACLE ATTACK//
    public Transform[] spawnArray;
    bool tentacleAttack;
    //BRAINSHOOT ATTACK//
    public GameObject littleBrain;
    public float littleBrainSpeed;
    public float timeBetweenShots = 2f;  
    int randomBrainPos;
    bool brainSpawnAttack;
    //BULLETHELL//
    public GameObject brainBullet;
    public float brainBulletSpeed;
    bool bulletHell;
    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;
    public Sprite wallOfBrains;
    // Start is called before the first frame update
    void Start()
    {
        brainIsAlive = true;
        StartCoroutine(AttackPatternRotation());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator TentacleAttack()
    {
        tentacleAttack = true;
        bulletHell = false;
        brainSpawnAttack = false;
        while (tentacleAttack == true)
        {
            yield return new WaitForSeconds(3);
            //Moves up and down while shooting out tentacles
            //3 spawnpoints in the middle of the boss
            // Find all GameObjects of a specific type
            GameObject[] tentacleSpawnPoints = GameObject.FindGameObjectsWithTag("TentacleSpawnP"); // You can use a tag or other criteria to identify your GameObjects

            // Loop through each GameObject and access its script
            foreach (GameObject tentacleSpawnPoint in tentacleSpawnPoints)
            {
                // Access the script attached to the GameObject
                Tentacle tentacleScript = tentacleSpawnPoint.GetComponent<Tentacle>();

                // Check if the script component is not null
                if (tentacleScript != null)
                {
                    Debug.Log("CallTentacles");
                    // Call the method on the script
                    StartCoroutine(tentacleScript.SpawnTentacles());
                }
            }
        }
    }
    public IEnumerator BrainSpawnAttack()
    {
        tentacleAttack = false;
        bulletHell = false;
        brainSpawnAttack = true;
        //Instantiating some brain enemies running towards our targets.
        yield return new WaitForSeconds(0);
        while (brainSpawnAttack == true)
        {
            randomBrainPos = UnityEngine.Random.Range(0, spawnArray.Length);
            Instantiate(littleBrain, spawnArray[randomBrainPos].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(0.3f);
        }
    }

    public IEnumerator SpinningBulletHell()
    {
        tentacleAttack = false;
        bulletHell = true;
        brainSpawnAttack = false;
        yield return new WaitForSeconds(0);
        //Spawning deadly projectiles in a fast pace while moving up and down
        //Instantiating a bunch of bullets giving them random directions and a high spawnrate
        while (bulletHell == true)
        {
            randomBrainPos = UnityEngine.Random.Range(0, spawnArray.Length);
            Instantiate(brainBullet, spawnArray[randomBrainPos].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(0.3f);
        }
    }

    public IEnumerator AttackPatternRotation()
    {
        //Wait X seconds, stop every coroutine, start next coroutine
        while (brainIsAlive)
        {
            Debug.Log("Brain is alive, attack pattern rotation starts!");

            // Start the next coroutine
            StartCoroutine(TentacleAttack());
            yield return new WaitForSeconds(20);

            // Stop the current coroutine
            StopCoroutine("TentacleAttack");
            yield return new WaitForSeconds(5);
            // Start the next coroutine
            StartCoroutine(BrainSpawnAttack());
            yield return new WaitForSeconds(20);

            // Stop the current coroutine
            StopCoroutine("BrainSpawnAttack");
            yield return new WaitForSeconds(5);
            // Start the next coroutine
            StartCoroutine(SpinningBulletHell());
            yield return new WaitForSeconds(20);

            // Stop the current coroutine
            StopCoroutine("SpinningBulletHell");
            yield return new WaitForSeconds(5);
        }
    }
    void Phase2()
    {
        //Turns into a wall of brains (eg changes sprites) to fill out the width of the level
        // Assign the new sprite to the SpriteRenderer component
        spriteRenderer.sprite = wallOfBrains;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
