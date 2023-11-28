using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBomb : MonoBehaviour
{
    public float speed = 10f;

    private Transform targetShooter;
    private Transform targetTaunter;
    public bool shooterChosen;
    public bool taunterChosen;
    public bool coroutineStarted;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        shooterChosen = false;
        taunterChosen = false;
        coroutineStarted = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetShooter = GameObject.Find("Shooter").GetComponent<Transform>();
        targetTaunter = GameObject.Find("Taunter").GetComponent<Transform>();
    }
    void Update()
    {
        //it only starts if the coroutine is still false
        //coroutine takes 10 speed of step every 2 seconds. should be continuous
        
        if (gameManager.isShooterChased && coroutineStarted == false)
          {
            StartCoroutine("ShooterFollow");
          }
        else if (gameManager.isTaunterChased && coroutineStarted == false)
          {
             StartCoroutine("TaunterFollow");
          }
        if (shooterChosen)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (taunterChosen)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public IEnumerator ShooterFollow()
    {
        coroutineStarted = true;
        taunterChosen = false;
        yield return new WaitForSeconds(2);
        shooterChosen = true;
        Debug.Log("Shooter is followed!");  
    }
    public IEnumerator TaunterFollow()
    {
        coroutineStarted = true;
        shooterChosen = false;
        yield return new WaitForSeconds(2);
        taunterChosen = true;
        Debug.Log("Taunter is followed!");
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
    }
}
