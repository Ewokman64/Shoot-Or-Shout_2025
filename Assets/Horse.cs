using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour

{
    public float speed = 1;
    public float horseHealth;
    private Transform targetShooter;
    private GameManager gameManager;
    private NightKnight nightKnight;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetShooter = GameObject.Find("Shooter").GetComponent<Transform>();
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            horseHealth--;

            // Check if the horseman should be destroyed
            if (horseHealth <= 0)
            {
                nightKnight.spearSpawnRate = 1;
                nightKnight.StartCoroutine("EquipShield");
                nightKnight.speed = 5;
                Destroy(gameObject);
            }

            // Destroy the bullet regardless of the health status
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Taunter"))
        {
            gameManager.isSomeoneDead = true;
            gameManager.GameOver();
        }
        if (other.gameObject.CompareTag("Shooter"))
        {
            gameManager.isSomeoneDead = true;
            gameManager.GameOver();
        }
    }
}
