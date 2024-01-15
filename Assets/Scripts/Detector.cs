﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Detector : MonoBehaviour
{
    public WallHealthBar wallHealthBar;
    public GameManager gameManager;
    public SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        wallHealthBar = GameObject.Find("WallHealth").GetComponent<WallHealthBar>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        int targetLayer = LayerMask.NameToLayer("Enemies");
        if (other.gameObject.layer == targetLayer && !other.gameObject.CompareTag("BigEnemy"))
        {
            spawnManager.enemyCount--;
            wallHealthBar.maxHealth--;
            wallHealthBar.UpdateHealthBar();
        }
        else if (other.gameObject.layer == targetLayer && other.gameObject.CompareTag("BigEnemy"))
        {
            spawnManager.enemyCount--;
            wallHealthBar.maxHealth -= 3;
            wallHealthBar.UpdateHealthBar();
        }
        Destroy(other.gameObject);
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
