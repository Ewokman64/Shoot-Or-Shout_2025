using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBomb : MonoBehaviour
{
    public float speed = 20;

    private Transform targetShooter;
    private Transform targetTaunter;
    private GameManager gameManager;
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
            StartCoroutine(ShooterFollow());
        }
        else
        {
            StartCoroutine(TaunterFollow());
        }
    }
    public IEnumerator ShooterFollow()
    {
        yield return new WaitForSeconds(3);

        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public IEnumerator TaunterFollow()
    {
        yield return new WaitForSeconds(3);
        transform.Translate(Vector2.right * speed * Time.deltaTime);
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
    }
}
