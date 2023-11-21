using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControls : MonoBehaviour
{    
    public float speed;

    private Transform targetShooter;
    private Transform targetTaunter;

    private GameManager gameManager;

    public ParticleSystem bloodSplash;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetShooter = GameObject.Find("Shooter").GetComponent<Transform>();
        targetTaunter = GameObject.Find("Taunter").GetComponent<Transform>();
    }
    void Update()
    {
     if (gameManager.isShooterChased && targetShooter != null)
        {
            Debug.Log("Shooter is followed");
            ShooterFollow();
        }
     else
        {
            Debug.Log("Taunter is followed");
            TaunterFollow();
        }
    }
    public void ShooterFollow()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetShooter.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
        {
        transform.position = Vector3.MoveTowards(transform.position, targetTaunter.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    void OnTriggerEnter2D(Collider2D other)
        {
        if (other.gameObject.CompareTag("Taunter"))
            {
            gameManager.isSomeoneDead = true;
            gameManager.GameOver();
            }
        if (other.gameObject.CompareTag("Shooter"))
            {
            gameManager.isSomeoneDead = true;
            gameManager.GameOver();
            }
        if (other.gameObject.CompareTag("Bullet"))
            {
            bloodSplash.Play();
            }
        }
}
