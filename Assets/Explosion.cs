using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private int enemiesLayer;
    public bool didSpawn = false;
    public GameObject explosionInstance;
    private GameManager gameManagerRef;
    private void Start()
    {
        gameManagerRef = GameObject.Find("GameManager").GetComponent<GameManager>();

        enemiesLayer = LayerMask.NameToLayer("Enemies");

        Debug.Log("Explosion");

        StartCoroutine(DestroyExplosion(this.gameObject));
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        EnemyStats enemyStats;
        if (other.gameObject.layer == enemiesLayer)
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();
            Destroy(other.gameObject);

            //Should be: for everyenemy it hits, add xy points
            gameManagerRef.UpdateNormalCurrency(20);
        }
        
    }

    public IEnumerator DestroyExplosion(GameObject instance)
    {
        yield return new WaitForSeconds(0.2f);

        Destroy(instance);
    }
}
