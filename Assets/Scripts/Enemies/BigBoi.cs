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
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        defaultColor = spriteRenderer.material.color;
        GameObject shooterObject = GameObject.Find("Shooter(Clone)");
        if (shooterObject != null)
        {
            targetShooter = shooterObject.GetComponent<Transform>();
        }
    }
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
        transform.Translate(Vector2.right * enemyStats.movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
    {
        transform.Translate(Vector2.right * enemyStats.movementSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            StartCoroutine(DamageColor());
        }
    }
    public IEnumerator DamageColor()
    {
        yield return null;
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.material.color = defaultColor;
    }
}
