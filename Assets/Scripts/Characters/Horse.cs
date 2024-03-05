using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Horse : MonoBehaviour

{
    public float speed = 1;
    public float horseHealth;
    private Transform targetShooter;
    private GameManager gameManager;
    private SpawnEnemies waveManager;
    private NightKnight nightKnight;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
        targetShooter = GameObject.Find("Shooter(Clone)").GetComponent<Transform>();
        nightKnight = GameObject.FindGameObjectWithTag("NightKnight").GetComponent<NightKnight>();
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
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    public void EnrageHorse()
    {
            // Detach all children from the parent
            foreach (Transform child in transform)
            {
                child.parent = null;
                speed = 5;
            }
            //horseEnraged = true;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            horseHealth--;

            if (horseHealth <= 0)
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
