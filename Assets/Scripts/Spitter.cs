using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    public GameObject acidBall;
    private float startDelay = 1;
    private float spawnRate = 3;
    private GameManager gameManager;
    private Transform targetShooter;
    private Transform targetTaunter;

    // Start is called before the first frame update
    void Start()
    {
        targetShooter = GameObject.Find("Shooter").GetComponent<Transform>();
        targetTaunter = GameObject.Find("Taunter").GetComponent<Transform>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("AcidBallSpawn", startDelay, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isShooterChased == true)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    void AcidBallSpawn()
    {
        Vector3 AcidBallPos = GameObject.FindGameObjectWithTag("AcidBallSpawnPoint").transform.position;
        Instantiate(acidBall, AcidBallPos, acidBall.transform.rotation);
    }
}
