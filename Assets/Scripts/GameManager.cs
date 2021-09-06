using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject restartButton;
    public GameObject restartMenu;  
    public AudioSource gmAudio;
    public AudioClip zombie;
    public TextMeshProUGUI soulEnergyText;
    public TextMeshProUGUI soulEnergyCollectedText;
    public TextMeshProUGUI currentRecordText;   
    public int score;
    public int currentRecord;
    public bool isSomeoneDead = false;
    public bool isTaunterChased;
    public bool isShooterChased;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        ScoreManager();       
    }
    void StartGame()
    {
        currentRecord = PlayerPrefs.GetInt("currentRecord", 0);       
        isTaunterChased = false;
        isShooterChased = true;
    }  
    public void GameOver()
    {
        if (currentRecord < score)
        {
            PlayerPrefs.SetInt("currentRecord", score);
        }      
        restartButton.SetActive(true);
        restartMenu.SetActive(true);
        soulEnergyCollectedText.text = "Soul Energy Collected: " + score;
        DestroyEnemies();
        gmAudio.mute = true;
    }
    public void ZombieSound()
    {
        gmAudio.PlayOneShot(zombie, 1.0f);
    }
    public void UpdateCurrency(int currencyToAdd)
    {
        score += currencyToAdd;
        soulEnergyText.text = "Soul Energy: " + score;
    }

    public void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject enemy in enemies)
        Destroy(enemy);
    }
    public void ScoreManager()
    {
        if (currentRecord < score) currentRecordText.text = "Current Record: " + score.ToString();
        else currentRecordText.text = "Current Record: " + currentRecord.ToString();
    }
}


