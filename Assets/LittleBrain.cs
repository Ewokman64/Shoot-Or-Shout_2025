using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBrain : MonoBehaviour
{
    public float speed = 7;

    private Transform targetShooter;
    private Transform targetTaunter;

    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetShooter = GameObject.Find("Shooter(Clone)").GetComponent<Transform>();
        targetTaunter = GameObject.Find("Taunter(Clone)").GetComponent<Transform>();

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
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void TaunterFollow()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
