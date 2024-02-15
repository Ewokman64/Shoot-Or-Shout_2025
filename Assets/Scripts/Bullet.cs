using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 30;
    public float bulletHealth;
    public float maxHealth = 1;
    public int zombieValue = 3;
    public bool isZombieShot = false;
    private GameManager gameManager;
    private AudioManager audioManager;
    private SpawnManager spawnManager;
    private NightKnight nightKnight;
    private Horse horse;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //LISTS TO COMPARE FROM MIGHT BE CLEANER
        int normalEnemies = LayerMask.NameToLayer("Enemies");
        int specialEnemies = LayerMask.NameToLayer("SpecialEnemies");
        int bossEnemies = LayerMask.NameToLayer("BossEnemies");

        if (other.gameObject.CompareTag("Spitter"))
        {
            SpawnEnemies spawnfillerenemies;
            spawnfillerenemies = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
            spawnfillerenemies.OnEnemyDestroyed(other.gameObject);
        }
        if (other.gameObject.layer == normalEnemies || other.gameObject.layer == specialEnemies || other.gameObject.CompareTag("Shield"))
        {
            gameManager.stallingTimer = 10;
        }
        if (other.gameObject.layer == normalEnemies || other.gameObject.layer == bossEnemies)
        {
            Destroy(other.gameObject, 0.2f);
            isZombieShot = true;
            audioManager.PlayZombieDeath();
            gameManager.UpdateNormalCurrency(zombieValue);
            if (other.gameObject == null)
            {
                gameManager.UpdateNormalCurrency(zombieValue);
            }
        }
        else if (other.gameObject.CompareTag("Shield"))
        {
            nightKnight = GameObject.FindGameObjectWithTag("NightKnight").GetComponent<NightKnight>();
            nightKnight.shieldHealth--;
            if (nightKnight.shieldHealth <= 0)
            {
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("NightKnight"))
        {
            nightKnight = GameObject.FindWithTag("NightKnight").GetComponent<NightKnight>();
            nightKnight.nightKnightHealth--;
        }
        else if (other.gameObject.CompareTag("Horse"))
        {
            horse = GameObject.Find("Horse").GetComponent<Horse>();
            horse.horseHealth--;
        }
        if (isZombieShot == true)
        {
            Invoke(nameof(SetBoolBack), 1.0f);
        }
        if (bulletHealth <= 0)
        {
            Destroy(gameObject);
        }
        bulletHealth--;
    }
    private void SetBoolBack()
    {
        isZombieShot = false;
    }
    public void SetHealth(float newHealth)
    {
        // Additional logic (if needed)
        bulletHealth = Mathf.Clamp(newHealth, 0f, maxHealth);
    }
    public float GetHealth()
    {
        return bulletHealth;
    }
}
