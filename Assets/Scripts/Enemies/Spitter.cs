using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    EnemyStats enemyStats;

    public Transform targetShooter;
    public Transform targetTaunter;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        StartCoroutine("ShootProjectile");
    }
    public IEnumerator ShootProjectile()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Instantiate(enemyStats.projectile, transform.position, enemyStats.projectile.transform.rotation);
            yield return new WaitForSeconds(enemyStats.rateOfFire);
        }      
    }
}
