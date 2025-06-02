using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int enemiesLayer;
    private float explosionDamage;
    public bool didSpawn = false;
    public GameObject explosionInstance;
    private GameManager gameManagerRef;
    EnemyArmor enemyArmor;
    EnemyStats enemyStats;
    private void Start()
    {
        gameManagerRef = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemiesLayer = LayerMask.NameToLayer("Enemies");

        Debug.Log("Explosion");

        StartCoroutine(DestroyExplosion(this.gameObject));
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        //EnemyArmor enemyArmor;
        if (other.gameObject.CompareTag("enemyArmor"))
        {
            enemyArmor = other.gameObject.GetComponent<EnemyArmor>();
            enemyArmor.armorHealth -= explosionDamage; //With upgrades, maybe we can make it insta destroyed

            if (enemyArmor.armorHealth <= 0)
            {
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.layer == enemiesLayer)
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();
            enemyStats.health -= explosionDamage;

            if (enemyStats.health <= 0)
            {
                Destroy(other.gameObject);
            }
        }
    }
    public IEnumerator DestroyExplosion(GameObject instance)
    {
        yield return new WaitForSeconds(0.2f);

        Destroy(instance);
    }
}
