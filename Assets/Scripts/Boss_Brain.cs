using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Brain : MonoBehaviour
{
    int health = 200;
    //TENTACLE ATTACK

    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;
    public Sprite wallOfBrains;

    //BRAINSHOOT ATTACK
    public GameObject littleBrain;
    public float littleBrainSpeed;
    public Transform[] littleBrainPos;
    //BULLETHELL
    public GameObject brainBullet;
    public float brainBulletSpeed;
    public Transform[] brainBulletPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(TentacleAttack());
        }
    }
    public IEnumerator TentacleAttack()
    {
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

    void Phase2()
    {
        //Turns into a wall of brains (eg changes sprites) to fill out the width of the level
        // Assign the new sprite to the SpriteRenderer component
        spriteRenderer.sprite = wallOfBrains;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
