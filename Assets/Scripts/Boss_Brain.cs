using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Brain : MonoBehaviour
{
    int health = 200;
    //TENTACLE ATTACK
    public GameObject tentacle;
    // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer;
    public Sprite wallOfBrains;
    public Transform[] tentaclePos;
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
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(1);
        //Turns into a wall of brains (eg changes sprites) to fill out the width of the level
        // Assign the new sprite to the SpriteRenderer component
        spriteRenderer.sprite = wallOfBrains;
        yield return new WaitForSeconds(5);
        //Instantiating tentacles and lenghten them to hit players
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
}
