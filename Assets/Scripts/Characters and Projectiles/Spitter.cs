﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    public GameObject acidBall;
    private float startDelay = 0.5f;
    public float spawnRate;
    
    public Transform targetShooter;
    public Transform targetTaunter;

    private GameManager gameManager;

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
        if (gameManager.isShooterChased == true && targetShooter != null)
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
        Instantiate(acidBall, transform.position, acidBall.transform.rotation);
    }
}
