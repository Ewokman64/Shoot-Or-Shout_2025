using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightKnight : MonoBehaviour
{
    public float speed = 1;
    public float nightKnightHealth;
    private Transform targetShooter;
    private Horse horse;
    private bool horseEnraged = false;
    public GameObject spear;
    public GameObject shield;
    public float shieldHealth = 10;
    public GameObject nightKnight;
    public float spearSpawnRate = 3.0f; // Default spawn rate
    public bool shieldEquipped = false;
    

    private GameManager gameManager;
    private SpawnEnemies waveManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
        GameObject shooterObject = GameObject.Find("Shooter(Clone)");
        if (shooterObject != null)
        {
            targetShooter = shooterObject.GetComponent<Transform>();
        }
        horse = GameObject.Find("Horse").GetComponent<Horse>();
        StartCoroutine(ThrowSpear());
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
    public IEnumerator ThrowSpear()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Instantiate(spear, transform.position, spear.transform.rotation);
            yield return new WaitForSeconds(spearSpawnRate);
        }
    }

    public IEnumerator EnrageKnight()
    {
        yield return null;

        // Offset the position in the direction the shield should appear
        Vector2 offset = new Vector2(1.0f, 0.0f); // Adjust this offset as needed
        Vector2 newPosition = (Vector2)transform.position + offset; // Calculate the new position

        // Instantiate the shield at the adjusted position
        GameObject newShield = Instantiate(shield, newPosition, shield.transform.rotation);

        // Set the child's parent to the specified parent GameObject
        newShield.transform.parent = nightKnight.transform;
        shieldEquipped = true;

        speed = 5;
        spearSpawnRate = 1.5f;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            nightKnightHealth--;

            if (nightKnightHealth <= 0)
            {
                waveManager.nightKnightDead = true;
                Destroy(gameObject);
                if (!waveManager.horseDead)
                {
                    horse.StartCoroutine("EnrageHorse");
                }
            }
        }
    }
}
