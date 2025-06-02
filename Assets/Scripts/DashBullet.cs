using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashBullet : MonoBehaviour
{
    private int enemiesLayer;

    private float bulletSpeed = 30;
    public float damage;
    Vector3 explosionPos;
    public GameObject explosionPrefab;
    public GameObject explosionInstance;

    private GameManager gameManagerRef;
    private AudioManager audioManager;

    public void Start()
    {
        gameManagerRef = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        enemiesLayer = LayerMask.NameToLayer("Enemies");
    }
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //**NEW CODE**
        //**THIS SHOULD BE A SEPARATE COLLISION SCRIPT LATER TO USE IT FOR OTHER PROJECTILES
        EnemyStats enemyStats;

        if (other.gameObject.layer == enemiesLayer)
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();

            DealDamage(other.gameObject);
            gameManagerRef.stallingTimer = 10;
            audioManager.PlayZombieDeath();

            //LET'S INSTANTIATE AN EXPLOSION PREFAB INSTEAD
            explosionPos = gameObject.transform.position;
            ExplodeOnImpact();

            if (enemyStats.health <= 0)
            {
                Destroy(other.gameObject);
                gameManagerRef.UpdateNormalCurrency(enemyStats.points);
            }

            //Look into this, no clue how it works
            if (enemyStats.name == "Spitter") //removes the spitter's spawnpoint from the occupied spawnpoint list
            {
                SpawnEnemies spawnfillerenemies;
                spawnfillerenemies = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
                spawnfillerenemies.OnEnemyDestroyed(other.gameObject);
            }
        }
        Destroy(gameObject);
    }
    public void DealDamage(GameObject enemy)
    {
        EnemyStats enemyStats = enemy.GetComponent<EnemyStats>();
        enemyStats.health -= damage;
    }
    public void ExplodeOnImpact()
    {
        Instantiate(explosionPrefab, explosionPos, gameObject.transform.rotation);
        audioManager.PlayShotExplosion();
    }
}
