using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private GameManager gameManager;
    public bool isShooterTargeted = true;
    public bool isTaunterTargeted;

    private ProjectileStats projectileStats;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        projectileStats = GetComponent<ProjectileStats>();

        projectileStats.currentSpeed = projectileStats.regularSpeed;

        ChooseTarget();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimeSlow();

        Move();
    }

    private void ChooseTarget()
    {
        if (gameManager.isShooterChased == true)
        {
            isShooterTargeted = true;
            isTaunterTargeted = false;
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (gameManager.isTaunterChased == true)
        {
            isTaunterTargeted = true;
            isShooterTargeted = false;
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void Move()
    {
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * projectileStats.currentSpeed);
        }
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.Translate(Vector3.right * Time.deltaTime * projectileStats.currentSpeed);
        }
    }

    private void CheckTimeSlow()
    {
        if (gameManager.isTimeSlowed)
        {
            projectileStats.currentSpeed = projectileStats.timeSlowSpeed;
        }
        else
        {
            projectileStats.currentSpeed = projectileStats.regularSpeed;
        }
    }
}
