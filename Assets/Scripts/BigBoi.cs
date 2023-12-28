using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBoi : MonoBehaviour
{
    public float speed = 2;
    public float health = 5;

    private Transform targetShooter;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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

        if (health <= 0)
        {
            Destroy(gameObject);
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Bullet hit!");
            Destroy(other.gameObject);
            health--;
        }
    }
}
