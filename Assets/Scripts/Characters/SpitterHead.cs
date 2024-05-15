using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpitterHead : MonoBehaviour
{
    EnemyStats enemyStats;

    public GameObject acidBall;
    public Transform acidBallSpawn;
    public Transform targetShooter;
    public Transform targetTaunter;
    public GameManager gameManager;
    public int speed = 1;
    public float health = 1;
    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        targetShooter = GameObject.Find("Shooter(Clone)").GetComponent<Transform>();
        targetTaunter = GameObject.Find("Taunter(Clone)").GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine("ShootAcidBall");
    }
    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * enemyStats.speed);
        if (gameManager.isShooterChased == true && targetShooter != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public IEnumerator ShootAcidBall()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Instantiate(acidBall, transform.position, acidBall.transform.rotation);
            yield return new WaitForSeconds(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            enemyStats.speed *= -1;
        }
    }
}
