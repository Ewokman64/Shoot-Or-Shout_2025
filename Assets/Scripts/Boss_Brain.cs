using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Brain : MonoBehaviour
{
    int health = 200;
    bool brainIsAlive;
    //TENTACLE ATTACK//
    public bool tentacleAttack;
    
    //BRAINSHOOT ATTACK//
    public GameObject littleBrain;
    public float littleBrainSpeed;
    public float timeBetweenShots = 2f;
    public Transform[] littleBrainPos;
    //BULLETHELL//
    public GameObject brainBullet;
    public float brainBulletSpeed;
    public Transform[] brainBulletPos;

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;
    public Sprite wallOfBrains;
    // Start is called before the first frame update
    void Start()
    {
        brainIsAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator TentacleAttack()
    {
        while (tentacleAttack)
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
        
        yield return new WaitForSeconds(1);     
    }
    public IEnumerator BrainShootAttack()
    {
        yield return new WaitForSeconds(10);
        //Instantiating some brain enemies running towards our targets.
       
    }

    public IEnumerator SpinningBulletHell()
    {
        yield return new WaitForSeconds(10);
        //Instantiating a bunch of bullets giving them random directions and a high spawnrate
    }

    /*public IEnumerator AttackPatternRotation()
    {
        yield return new WaitForSeconds(10);
        //Wait X seconds, stop every coroutine, start next coroutine
        while (brainIsAlive)
        {
            StartCoroutine(TentacleAttack());
            yield return new WaitForSeconds(5);
            StartCoroutine(BrainShootAttack());
            yield return new WaitForSeconds(5);
            StartCoroutine(SpinningBulletHell());
        }
    }*/

    void Phase2()
    {
        //Turns into a wall of brains (eg changes sprites) to fill out the width of the level
        // Assign the new sprite to the SpriteRenderer component
        spriteRenderer.sprite = wallOfBrains;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
