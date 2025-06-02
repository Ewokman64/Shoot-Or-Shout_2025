using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int enemiesLayer;

    private float bulletSpeed = 30;
    public float piercePower = 1;
    public float damage;
    public float defaultPierce = 1;
    //public int zombieValue = 3;
    public bool isZombieShot = false;
    private GameManager gameManager;
    private AudioManager audioManager;
    private SpawnManager spawnManager;
    private NightKnight nightKnight;
    private Horse horse;

    // Start is called before the first frame update
    void Start()
    {
        enemiesLayer = LayerMask.NameToLayer("Enemies");
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
        //**NEW CODE**
        //**THIS SHOULD BE A SEPARATE COLLISION SCRIPT LATER TO USE IT FOR OTHER PROJECTILES
        EnemyStats enemyStats;

        if (other.gameObject.CompareTag("EnemyArmor"))
        {
            EnemyArmor enemyArmor = other.gameObject.GetComponent<EnemyArmor>();
            enemyArmor.armorHealth--;
            Destroy(gameObject);
            return;
        }
        if (other.gameObject.layer == enemiesLayer)
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();

            DealDamage(other.gameObject);
            gameManager.stallingTimer = 10;
            isZombieShot = true;
            audioManager.PlayZombieDeath();

            piercePower--;

            if (piercePower <= 0)
            {
                Destroy(gameObject);
            }

            if (enemyStats.health <= 0)
            { 
                Destroy(other.gameObject);
                gameManager.UpdateNormalCurrency(enemyStats.points); 
            }
            //Look into this, no clue how it works
            if (enemyStats.name == "Spitter") //removes the spitter's spawnpoint from the occupied spawnpoint list
            {
                SpawnEnemies spawnfillerenemies;
                spawnfillerenemies = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
                spawnfillerenemies.OnEnemyDestroyed(other.gameObject);
            }
        }
    }
    public void DealDamage(GameObject enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        enemyStats.health -= damage;
    }
    public void SetDefaultPierce()
    {
        piercePower = defaultPierce;
    }
    public float GetHealth()
    {
        return piercePower;
    }
}
