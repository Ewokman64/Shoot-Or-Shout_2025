using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    private SpawnManager spawnManager;
    public WallHealthBar wallHealthBar;
    public GameManager gameManager;
    private UpgradeList upgradeList;
    private UpgradesManager upgradesManager;
    public float stallingTimer;
    //UI
    public GameObject gameOverScreen;
    public GameObject startScreenCanvas;
    public GameObject demoOverScreen;
    public GameObject pauseMenu;
    public GameObject stallingText;

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
    //INTS
    public int score;
    public int currentRecord;
    //bools
    public bool isSomeoneDead = false;
    public bool isTaunterChased;
    public bool isShooterChased;
    public bool isStallingActive;
    private bool gameCanBePaused = false;
    public bool hasGameStarted;
    public bool bossDied;
    void Start()
    {
        isShooterChased = true;
        isStallingActive = false;
        hasGameStarted = false;
        bossDied = false;
        stallingTimer = 10;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        wallHealthBar = GameObject.Find("WallHealth").GetComponent<WallHealthBar>();
        upgradeList = GameObject.Find("UpgradesManager").GetComponent<UpgradeList>();
        upgradesManager = GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
        upgradeList.enabled = false;
    }

    void Update()
    {   
        scoreManager();
        if (bossDied)
        {
            demoOverScreen.SetActive(true);
        }
        if (upgradesManager.upgradePanel.activeSelf)
        {
            // Pause the game
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game
            Time.timeScale = 1;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        if (wallHealthBar.maxHealth < 0)
        {
            gameManager.GameOver();
        }
        if (stallingTimer <= 0)
        {
            isStallingActive = true;
        }
        else if (stallingTimer > 0)
        {
            isStallingActive = false;
            stallingText.SetActive(false);
        }
        if (hasGameStarted)
        {
            stallingTimer -= Time.deltaTime;
        }
    }
    public void StartGame()
    {
        //"previousEasyScore" key is getting loaded"
        currentRecord = PlayerPrefs.GetInt("previousNormalScore", 0);

        spawnManager.StartSpawnManager();

        isTaunterChased = false;
        isShooterChased = true;
        hasGameStarted = true;
        isSomeoneDead = false;

        startScreenCanvas.SetActive(false);
        upgradeList.enabled = true;

        gmAudio.clip = bgMusic;
        gmAudio.Play();
        
        StartCoroutine(StartStallingPenalty());
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

        string[] enemyTags = { "Enemy", "BigEnemy", "Spitter", "NightKnight", "Horse", "FinalPush", "Boss" /* Add more tags as needed */ };

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

    public IEnumerator StartStallingPenalty()
    {
        while (true)
        {
            if (isStallingActive)
            {
                wallHealthBar.maxHealth--;
                wallHealthBar.UpdateHealthBar();
                stallingText.SetActive(true);
            }
            yield return new WaitForSeconds(2);
        }
        
    }
}


