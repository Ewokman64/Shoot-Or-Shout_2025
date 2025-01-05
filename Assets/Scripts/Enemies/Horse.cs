using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Horse : MonoBehaviour

{
    //public float speed = 1;
    //public float horseHealth;
    private Transform targetShooter;
    private GameManager gameManager;
    private SpawnEnemies waveManager;
    private NightKnight nightKnight;
    public GameObject parentObject;
    EnemyStats enemyStat;
    // Start is called before the first frame update
    void Start()
    {
        enemyStat = GetComponent<EnemyStats>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
        targetShooter = GameObject.Find("Shooter(Clone)").GetComponent<Transform>();
        nightKnight = GameObject.FindGameObjectWithTag("NightKnight").GetComponent<NightKnight>();
        parentObject = transform.parent.gameObject;
    }

    // Update is called once per frame
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
        transform.Translate(Vector2.right * enemyStat.movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
    {
        transform.Translate(Vector2.right * enemyStat.movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void EnrageHorse()
    {
        enemyStat.movementSpeed = 5;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            enemyStat.health--;

            if (enemyStat.health-- <= 0)
            {
                waveManager.horseDead = true;
                Destroy(gameObject);
                if (!waveManager.nightKnightDead)
                {
                    nightKnight.StartCoroutine("EnrageKnight");
                }
            }
        }
    }
}
