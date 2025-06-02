using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    EnemyStats enemyStats;

    private Transform targetShooter;

    private GameManager gameManager;

    public SpriteRenderer spriteRenderer;
    private Color defaultColor;

    bool isDamageOverTimeActive = false;
    bool damageOverTimeChecked;

    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        GameObject shooterObject = GameObject.Find("Shooter(Clone)");

        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;

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

            spriteRenderer.material.color = Color.cyan;
        }
        else if (!gameManager.isSlowActive)
        {
            enemyStats.currentSpeed = enemyStats.regularSpeed;
            spriteRenderer.material.color = defaultColor;
        }
    }

    public void StartDamageOverTime()
    {
        if (isDamageOverTimeActive && !damageOverTimeChecked)
        {
            StartCoroutine(TakeDamageOverTime(enemyStats.health));
            damageOverTimeChecked = true;
        }
    } 

    public IEnumerator TakeDamageOverTime(float health)
    {
        while (isDamageOverTimeActive)
        {
            health--;
        }
        yield return new WaitForSeconds(0.5f);
    }
}
