using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControls : MonoBehaviour
{
    public ParticleSystem bloodSplash;
    public float speed = 6.0f;
    private Transform targetShooter;
    private Transform targetTaunter;
    private GameManager gameManager;
    private TaunterController taunterController;
    private ShooterController shooterController;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        taunterController = GameObject.Find("Taunter").GetComponent<TaunterController>();
        shooterController = GameObject.Find("Shooter").GetComponent<ShooterController>();
        targetShooter = GameObject.Find("Shooter").GetComponent<Transform>();
        targetTaunter = GameObject.Find("Taunter").GetComponent<Transform>();
    }
    void Update()
    {
     if (gameManager.isShooterChased)
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
        gameManager.isShooterChased = true;
        gameManager.isTaunterChased = false;
        transform.position = Vector3.MoveTowards(transform.position, targetShooter.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    public void TaunterFollow()
        {
        gameManager.isTaunterChased = true;
        gameManager.isShooterChased = false;
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
