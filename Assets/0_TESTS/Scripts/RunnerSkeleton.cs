using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerSkeleton : MonoBehaviour
{
    EnemyStats enemyStats;

    private Transform targetShooter;
    private GameManager_V2 gameManagerScript;

    public ParticleSystem bloodSplash;

    // Start is called before the first frame update
    void Start()
    {
        //We access this so we can access our stats
        enemyStats = GetComponent<EnemyStats>();
        
        //We access this so we can check which character is being chased
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager_V2>();

        //We find our Shooter and store it in this GameObject
        GameObject shooterObject = GameObject.Find("Shooter(Clone)");

        //If the Shooter exists, get its Transform component, a.k.a. its position
        if (shooterObject != null)
        {
            targetShooter = shooterObject.GetComponent<Transform>();
        }
    }
    void Update()
    {
        //If the isShooterChased bool is true and the target Shooter can be found in the scene, we run ShooterFollow.
        if (gameManagerScript.isShooterChased && targetShooter != null)
        {
            ShooterFollow();
        }
        else
        {
            TaunterFollow();
        }
    }
    public void ShooterFollow()
    {
        transform.Translate(Vector2.right * enemyStats.currentSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
    {
        transform.Translate(Vector2.right * enemyStats.currentSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            bloodSplash.Play();
        }
    }
}
