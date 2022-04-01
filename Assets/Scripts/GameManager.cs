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
        //"previousEasyScore" key is getting loaded"
        easyCurrentRecord = PlayerPrefs.GetInt("previousEasyScore", 0);
        normalCurrentRecord = PlayerPrefs.GetInt("previousNormalScore", 0);
        hardCurrentRecord = PlayerPrefs.GetInt("previousHardScore", 0);
        isTaunterChased = false;
        isShooterChased = true;
        startScreenCanvas.SetActive(false);
        gmAudio.clip = bgMusic;
        gmAudio.Play();
        isSomeoneDead = false;
    }
    public void GameOver()
    {
        int previousEasyScore = PlayerPrefs.GetInt("previousEasyScore");
        int previousNormalScore = PlayerPrefs.GetInt("previousNormalScore");
        int previousHardScore = PlayerPrefs.GetInt("previousHardScore");
        //code for setting highscore
        //"previousEasyScore" key is getting saved"
        if (difficultyManager.easyMode == true)
        {
            PlayerPrefs.SetInt("previousEasyScore", easyScore);
        }
        else if (difficultyManager.normalMode == true)
        {
            PlayerPrefs.SetInt("previousNormalScore", normalScore);
        }
        else if (difficultyManager.hardMode == true)
        {
            PlayerPrefs.SetInt("previousHardScore", hardScore);
        }
        DestroyEverything();
        gameOverScreen.SetActive(true);
        Destroy(GameObject.FindWithTag("Shooter"));
        Destroy(GameObject.FindWithTag("Taunter"));
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
        gmAudio.mute = true;

    }
    public void UpdateEasyCurrency(int easyCurrencyToAdd)
    {
        easyScore += easyCurrencyToAdd;
        soulEnergyText.text = "Soul Energy: " + easyScore;
    }
    public void UpdateNormalCurrency(int normalCurrencyToAdd)
    {
        normalScore += normalCurrencyToAdd;
        soulEnergyText.text = "Soul Energy: " + normalScore;
    }
    public void UpdateHardCurrency(int hardCurrencyToAdd)
    {
        hardScore += hardCurrencyToAdd;
        soulEnergyText.text = "Soul Energy: " + hardScore;
    }

    public void DestroyEverything()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        foreach (GameObject bullet in bullets)
            Destroy(bullet);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Zombie");
        foreach (GameObject enemy in enemies)
            Destroy(enemy);  
    }
    public void DestroyZombies()
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


