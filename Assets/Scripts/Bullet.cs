﻿using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int enemiesLayer;

    private float bulletSpeed = 30;
    public float piercePower = 1;
    public float defaultPierce = 1;
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
        
        if (other.gameObject.layer == enemiesLayer)
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();
            enemyStats.health--;
            gameManager.stallingTimer = 10;
            isZombieShot = true;
            audioManager.PlayZombieDeath();
            
            if (enemyStats.health <= 0)
            { 
                Destroy(other.gameObject);
                gameManager.UpdateNormalCurrency(enemyStats.points); 
            }

            if (enemyStats.name == "BossV1")
            {
                HealthBar healthBar = other.gameObject.GetComponent<HealthBar>();
                Boss_Brain bossScript = other.gameObject.GetComponent<Boss_Brain>();
                healthBar.UpdateHealthBar();
                if (enemyStats.health <= 0)
                {
                    bossScript.secondPhaseStarted = true;
                    StartCoroutine(bossScript.BossSecondPhase());
                }
            }

            if (enemyStats.name == "BossV2")
            {
                HealthBar healthBar = other.gameObject.GetComponent<HealthBar>();
                Boss_Brain_2ndPhase bossScript = other.gameObject.GetComponent<Boss_Brain_2ndPhase>();
                healthBar.UpdateHealthBar();
                if (enemyStats.health <= 0)
                {
                    bossScript.ClearMap();
                    gameManager.BossDied();
                }
            }

            if (enemyStats.name == "Spitter") //removes the spitter's spawnpoint from the occupied spawnpoint list
            {
                SpawnEnemies spawnfillerenemies;
                spawnfillerenemies = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
                spawnfillerenemies.OnEnemyDestroyed(other.gameObject);
            }

            piercePower--;

            if (piercePower <= 0)
            {
                Destroy(gameObject);
            }
        }

        //**OLD CODE**

        
        /*if (isZombieShot == true)
        {
            Invoke(nameof(SetBoolBack), 1.0f);
        }*/
    }
    private void SetBoolBack()
    {
        isZombieShot = false;
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
