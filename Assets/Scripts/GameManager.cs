using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public GameObject startScreenCanvas;
    public GameObject shooter;
    public GameObject taunter;
    public AudioSource gmAudio;
    public AudioClip bgMusic;
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
        
    }

    void Update()
    {
        ScoreManager();
    }
    public void StartGame()
    {
        currentRecord = PlayerPrefs.GetInt("currentRecord", 0);
        isTaunterChased = false;
        isShooterChased = true;
        shooter.SetActive(true);
        taunter.SetActive(true);
        startScreenCanvas.SetActive(false);
        gmAudio.clip = bgMusic;
        gmAudio.Play();
    }
    public void GameOver()
    {
        if (currentRecord < score)
        {
            PlayerPrefs.SetInt("currentRecord", score);
        }
        gameOverScreen.SetActive(true);
        shooter.SetActive(false);
        taunter.SetActive(false);
        soulEnergyCollectedText.text = "Soul Energy Collected: " + score;
        DestroyEnemies();
        gmAudio.mute = true;

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


