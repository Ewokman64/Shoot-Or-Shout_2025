using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBrain : MonoBehaviour
{
    EnemyStats enemyStats;

    //public float speed = 7;

    private Transform targetShooter;
    private Transform targetTaunter;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetShooter = GameObject.Find("Shooter(Clone)").GetComponent<Transform>();
        targetTaunter = GameObject.Find("Taunter(Clone)").GetComponent<Transform>();
        StartCoroutine(AutoDestroy());
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
        transform.Translate(Vector2.right * enemyStats.speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
    {
        transform.Translate(Vector2.right * enemyStats.speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public IEnumerator AutoDestroy()
    {
        yield return new WaitForSeconds(5);

        Destroy(gameObject);
    }
}
