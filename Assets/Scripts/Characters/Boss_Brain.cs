using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Brain : MonoBehaviour
{
    public HealthBar healthBar;
    int speed = 2;
    public bool brainIsAlive;
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
    //SECOND PHASE//
    public GameObject brainBoss_2;
    public bool secondPhaseStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        brainIsAlive = true;
        
        StartCoroutine(AttackPatternRotation());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
        if (healthBar.currentHealth <= 0 && !secondPhaseStarted)
        {
            StartCoroutine(BossSecondPhase());
            secondPhaseStarted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.CompareTag("Wall"))
        {
            speed *= -1;
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Boss hit");
            healthBar.currentHealth--;
            healthBar.UpdateHealthBar();
        }
    }
        public IEnumerator TentacleAttack()
    {
        Debug.Log("Tentacle Attack started!");
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
        Debug.Log("Brain Spawn started!");
        tentacleAttack = false;
        bulletHell = false;
        brainSpawnAttack = true;
        //Instantiating some brain enemies running towards our targets.
        yield return new WaitForSeconds(0);
        while (brainSpawnAttack == true)
        {
            randomBrainPos = UnityEngine.Random.Range(0, spawnArray.Length);
            Instantiate(littleBrain, spawnArray[randomBrainPos].position, UnityEngine.Quaternion.identity);

            yield return new WaitForSeconds(2f);
        }
    }

    public IEnumerator SpinningBulletHell()
    {
        Debug.Log("Bullet Hell started!");
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

            yield return new WaitForSeconds(0.5f);
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
            yield return new WaitForSeconds(5);

            // Stop the current coroutine
            StopCoroutine("TentacleAttack");
            yield return new WaitForSeconds(8);
            // Start the next coroutine
            StartCoroutine(BrainSpawnAttack());
            yield return new WaitForSeconds(5);

            // Stop the current coroutine
            StopCoroutine("BrainSpawnAttack");
            yield return new WaitForSeconds(5);
            // Start the next coroutine
            StartCoroutine(SpinningBulletHell());
            yield return new WaitForSeconds(5);

            // Stop the current coroutine
            StopCoroutine("SpinningBulletHell");
            yield return new WaitForSeconds(5);
        }
        yield return new WaitForSeconds(3);
    }
    public IEnumerator BossSecondPhase()
    {
        Instantiate(brainBoss_2, brainBoss_2.transform.position, UnityEngine.Quaternion.identity);
        Destroy(gameObject);
        yield return new WaitForSeconds(3);
    }
}
