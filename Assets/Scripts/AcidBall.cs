using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidBall : MonoBehaviour
{

    private float acidBallSpeed = 10;
    private GameManager gameManager;
    public bool isShooterTargeted;
    public bool isTaunterTargeted;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isShooterChased == true)
        {
            isShooterTargeted = true;
        }
        else
        {
            isTaunterTargeted = true;
        }
        if (isShooterTargeted == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * acidBallSpeed);
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * acidBallSpeed);
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Detector"))
        {
            Destroy(gameObject);
        }
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
