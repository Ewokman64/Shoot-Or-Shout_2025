using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBomb : MonoBehaviour
{
    public float speed = 20;

    private Transform targetShooter;
    private Transform targetTaunter;
    public Transform leftWall;
    public Transform rightWall;
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
            ShooterFollow();
        }
        else
        {
            TaunterFollow();
        }
    }
    public IEnumerator ShooterFollow()
    {
        yield return new WaitForSeconds(3);
        //rightWall = GameObject.Find("RightWall").GetComponent<Transform>();
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public IEnumerator TaunterFollow()
    {
        yield return new WaitForSeconds(3);
        //leftWall = GameObject.Find("leftWall").GetComponent<Transform>();
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 180, 0);
    }
}
