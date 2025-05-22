using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoi : MonoBehaviour
{
    EnemyStats enemyStats;
    private int specEnemyValue = 15;

    private Transform targetShooter;

    private GameManager gameManager;
    public SpriteRenderer spriteRenderer;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        defaultColor = spriteRenderer.material.color;      
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(DamageColor());
        }
    }

    //Could be implemented to all instead***
    public IEnumerator DamageColor()
    {
        yield return null;
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.material.color = defaultColor;
    }
}
