using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    //UI
    public GameObject gameOverScreen;
    public GameObject startScreenCanvas;
    public GameObject demoOverScreen;
    public GameObject pauseMenu;
    public GameObject[] enemies;

    public GameObject shooter;
    public GameObject taunter;
    //Audio
    public AudioSource gmAudio;
    public AudioClip bgMusic;
    //TEXTS
    public TextMeshProUGUI soulEnergyText;
    public TextMeshProUGUI soulEnergyCollectedText;
    public TextMeshProUGUI recordText;
    //scores
    public int score;

    public int currentRecord;

    //bools
    public bool isSomeoneDead = false;
    public bool isTaunterChased;
    public bool isShooterChased;
    void Start()
    {
        isShooterChased = true;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {   
        scoreManager();
        if (spawnManager.finalPushOver)
        {
            demoOverScreen.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }
    public void StartGame()
    {
        //"previousEasyScore" key is getting loaded"
        currentRecord = PlayerPrefs.GetInt("previousNormalScore", 0);
        spawnManager.StartSpawnManager();
        isTaunterChased = false;
        isShooterChased = true;

        startScreenCanvas.SetActive(false);

        gmAudio.clip = bgMusic;
        gmAudio.Play();

        isSomeoneDead = false;
    }
    public void GameOver()
    {
        Debug.Log("GAME OVER!");
        isSomeoneDead = true;
        int previousNormalScore = PlayerPrefs.GetInt("previousNormalScore");
        //code for setting highscore
        //"previousEasyScore" key is getting saved"
            PlayerPrefs.SetInt("previousNormalScore", score);
        //We set up the game over, clearing all objects

        gameOverScreen.SetActive(true);
        Destroy(GameObject.FindWithTag("Shooter"));
        Destroy(GameObject.FindWithTag("Taunter"));

        ClearMap();

        soulEnergyCollectedText.text = "Soul Energy Collected: " + score;
    }

    public void TogglePauseMenu()
    {
        // Toggle the pause state
        if (Time.timeScale == 0)
        {
            // Resume the game
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
        else
        {
            // Pause the game
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    public void ClearMap()
    {
        Debug.Log("CLEAR MAP HAS RAN");
        //GET RID OF BULLETS
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
            Destroy(bullet);

        //GET RID OF ENEMIES
        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //foreach (GameObject enemy in enemies)
        //    Destroy(enemy);

        string[] enemyTags = { "Enemy", "BigEnemy", "Spitter", "NightKnight", "Horse", "FinalPush" /* Add more tags as needed */ };

        foreach (string tag in enemyTags)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
    public void UpdateNormalCurrency(int normalCurrencyToAdd)
    {
        score += normalCurrencyToAdd;
        soulEnergyText.text = "Soul Energy: " + score;
    }
    public void scoreManager()
    {
        if (currentRecord < score) recordText.text = "Normal Record: " + score.ToString();
        else recordText.text = "Normal: " + currentRecord.ToString();
    }
}


