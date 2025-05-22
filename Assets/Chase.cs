using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    EnemyStats enemyStats;

    private Transform targetShooter;

    private GameManager gameManager;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        GameObject shooterObject = GameObject.Find("Shooter(Clone)");

        //Making sure shooter exists, then finding it's position
        if (shooterObject != null)
        {
            targetShooter = shooterObject.GetComponent<Transform>();
        }
    }
    void Update()
    {
        CheckSlow();

        if (enemyStats.movementType == "Regular")
        {
            ChooseTarget();
        }
    }

    void ChooseTarget()
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
        transform.Translate(Vector2.right * enemyStats.currentSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
    {
        transform.Translate(Vector2.right * enemyStats.currentSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void CheckSlow()
    {
        if (gameManager.isSlowActive)
        {
            enemyStats.currentSpeed = enemyStats.slowSpeed;
        }
        else if (!gameManager.isSlowActive)
        {
            enemyStats.currentSpeed = enemyStats.regularSpeed;
        }
    }
}
