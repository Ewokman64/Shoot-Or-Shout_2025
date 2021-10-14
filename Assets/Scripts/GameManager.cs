using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    private DifficultyManager difficultyManager;
    public GameObject gameOverScreen;
    public GameObject startScreenCanvas;
    public GameObject shooter;
    public GameObject taunter;
    public AudioSource gmAudio;
    public AudioClip bgMusic;
    public TextMeshProUGUI soulEnergyText;
    public TextMeshProUGUI soulEnergyCollectedText;
    public TextMeshProUGUI easyRecordText;
    public TextMeshProUGUI normalRecordText;
    public TextMeshProUGUI hardRecordText;
    public int easyScore;
    public int normalScore;
    public int hardScore;
    public int easyCurrentRecord;
    public int normalCurrentRecord;
    public int hardCurrentRecord;
    public bool isSomeoneDead = false;
    public bool isTaunterChased;
    public bool isShooterChased;

    void Start()
    {
        difficultyManager = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
    }

    void Update()
    {   
        EasyScoreManager();
        NormalScoreManager();
        HardScoreManager();
    }
    public void StartGame()
    {
        easyCurrentRecord = PlayerPrefs.GetInt("Easy Record", 0);
        normalCurrentRecord = PlayerPrefs.GetInt("Normal Record", 0);
        hardCurrentRecord = PlayerPrefs.GetInt("Hard Record", 0);
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
        if (easyCurrentRecord < easyScore)
        {
            PlayerPrefs.SetInt("easyRecord", easyScore);
        }
        if (normalCurrentRecord < normalScore)
        {
            PlayerPrefs.SetInt("normalRecord", normalScore);
        }
        if (hardCurrentRecord < hardScore)
        {
            PlayerPrefs.SetInt("hardRecord", hardScore);
        }
        gameOverScreen.SetActive(true);
        shooter.SetActive(false);
        taunter.SetActive(false);
        if (difficultyManager.easyMode == true)
        {
            soulEnergyCollectedText.text = "Soul Energy Collected: " + easyScore;
        }
        if (difficultyManager.normalMode == true)
        {
            soulEnergyCollectedText.text = "Soul Energy Collected: " + normalScore;
        }
        if (difficultyManager.hardMode == true)
        {
            soulEnergyCollectedText.text = "Soul Energy Collected: " + hardScore;
        }
        DestroyEnemies();
        gmAudio.mute = true;

    }
    public void UpdateEasyCurrency(int currencyToAdd)
    {
        easyScore += currencyToAdd;
        soulEnergyText.text = "Soul Energy: " + easyScore;
    }
    public void UpdateNormalCurrency(int currencyToAdd)
    {
        normalScore += currencyToAdd;
        soulEnergyText.text = "Soul Energy: " + normalScore;
    }
    public void UpdateHardCurrency(int currencyToAdd)
    {
        hardScore += currencyToAdd;
        soulEnergyText.text = "Soul Energy: " + hardScore;
    }

    public void DestroyEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);
    }
    public void EasyScoreManager()
    {
        if (easyCurrentRecord < easyScore) easyRecordText.text = "Easy Record: " + easyScore.ToString();
        else easyRecordText.text = "Easy Record: " + easyCurrentRecord.ToString();
    }
    public void NormalScoreManager()
    {
        if (normalCurrentRecord < normalScore) normalRecordText.text = "Normal Record: " + normalScore.ToString();
        else normalRecordText.text = "Normal: " + normalCurrentRecord.ToString();
    }
    public void HardScoreManager()
    {
        if (hardCurrentRecord < hardScore) hardRecordText.text = "Hard Record: " + hardScore.ToString();
        else hardRecordText.text = "Hard Record: " + hardCurrentRecord.ToString();
    }
}


