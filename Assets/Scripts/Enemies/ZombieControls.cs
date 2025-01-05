using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControls : MonoBehaviour
{
    EnemyStats enemyStats;

    private Transform targetShooter;

    private GameManager gameManager;

    public ParticleSystem bloodSplash;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        GameObject shooterObject = GameObject.Find("Shooter(Clone)");
        if (shooterObject != null)
        {
            targetShooter = shooterObject.GetComponent<Transform>();
        }
        //targetShooter = GameObject.Find("Shooter(Clone)").GetComponent<Transform>();
         //targetTaunter = GameObject.Find("Taunter(Clone)").GetComponent<Transform>();
    }
    void Update()
    {
     if (gameManager.isShooterChased && targetShooter != null)
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
        transform.Translate(Vector2.right * enemyStats.movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
    {
        transform.Translate(Vector2.right * enemyStats.movementSpeed * Time.deltaTime);
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
