using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int playerHealth;
    public TextMeshProUGUI playerHealthText;
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    [Header("References")]
    private SpawnManager spawnManager;
    public WallHealthBar wallHealthBar;
    public GameManager gameManager;
    private PowerupList upgradeList;
    private CountDown countDown;
    private SpawnEnemies waveManager;
    PowerUpDisplay powerupDisplayRef;
    public Bullet bulletPrefab; // Reference to the Bullet script attached to a prefab
    public float stallingTimer;
    //UI
    public GameObject gameOverScreen;
    public GameObject startScreenCanvas;
    public GameObject demoOverScreen;
    public GameObject pauseMenu;
    public GameObject stallingText;
    public GameObject youCanLeaveNowScreen;

    //public GameObject[] enemies;
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
    public float scoreMultiplier;
    //bools
    public bool isSomeoneDead = false;
    public bool isTaunterChased;
    public bool isShooterChased;
    public bool isStallingActive;
    public bool hasGameStarted;
    public bool gameIsPaused = false;

    public GameObject[] projectiles; //adding them in a list so they can be slowed by the Shouter

    public bool isTimeSlowed = false;

    void Start()
    {
        // THIS PLAYER DATA CAN BE HANDLED ELSEWHERE
        playerHealth = 3;
        playerHealthText.text = "Health: " + playerHealth;
        //
        waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
        Time.timeScale = 0;
        isShooterChased = true;
        isStallingActive = false;
        hasGameStarted = false;
        stallingTimer = 10;

        
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        wallHealthBar = GameObject.Find("WallHealth").GetComponent<WallHealthBar>();
        upgradeList = GameObject.Find("PowerupManager").GetComponent<PowerupList>();
        powerupDisplayRef = GameObject.Find("PowerupManager").GetComponent<PowerUpDisplay>();
        countDown = GetComponent<CountDown>();
        upgradeList.enabled = false;
    }

    void Update()
    {
        //UpdateEnemyList();
        scoreManager();
        if (Input.GetKeyDown(KeyCode.Space) && hasGameStarted == false)
        {
            StartGame();
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
        if (Time.timeScale == 0)
        {
            gameIsPaused = true;
        }
        else
        {
            gameIsPaused = false;
        }

        projectiles = GameObject.FindGameObjectsWithTag("Projectile");
    }
    public void StartGame()
    {
        //"previousEasyScore" key is getting loaded" 
        currentRecord = PlayerPrefs.GetInt("previousNormalScore", 0);
        waveManager.StartWaves();
        spawnManager.StartSpawnManager();
        Time.timeScale = 1;

        isTaunterChased = false;
        isShooterChased = true;

        hasGameStarted = true;
        isSomeoneDead = false;

        startScreenCanvas.SetActive(false);
        upgradeList.enabled = true;

        gmAudio.clip = bgMusic;
        gmAudio.Play();
        
        StartCoroutine(StartStallingPenalty());
        countDown.GetPlayerCD();
        
        PowerupList powerupListRef = GameObject.Find("PowerupManager").GetComponent<PowerupList>();
        powerupListRef.ResetStats();
    }
    public void GameOver()
    {
        isSomeoneDead = true;
        int previousNormalScore = PlayerPrefs.GetInt("previousNormalScore");
        //code for setting highscore
        //"previousEasyScore" key is getting saved"
            PlayerPrefs.SetInt("previousNormalScore", score);
        //We set up the game over, clearing all objects

        gameOverScreen.SetActive(true);
        Destroy(GameObject.FindWithTag("Shooter"));
        Destroy(GameObject.FindWithTag("Taunter"));
        Destroy(GameObject.FindWithTag("Laser"));
        gmAudio.mute = true;
        ClearMap();
        ClearBosses();
        //SetStatsBack();
        spawnManager.StopAllCoroutines();
        waveManager.StopAllCoroutines();
        StopCoroutine(StartStallingPenalty());
        soulEnergyCollectedText.text = "Soul Energy Collected: " + score;
    }

    public void TogglePauseMenu()
    {
        // Toggle the pause state
        if (Time.timeScale == 0)
        {
            // Resume the game
            Time.timeScale = 1;
            gameIsPaused = false;
            pauseMenu.SetActive(false);
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            gameIsPaused = true;
            pauseMenu.SetActive(true);
        }
    }

    public void ClearMap()
    {
        //GET RID OF BULLETS
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
            Destroy(bullet);

        //GET RID OF ENEMIES AND PROJECTILES
        string[] enemyTags = { "Enemy", "BigEnemy", "Spitter", "Projectile" /* Add more tags as needed */ };

        foreach (string tag in enemyTags)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }

    public void ClearBosses()
    {
        //GET RID OF BOSSES
        string[] enemyTags = { "FinalPush", "NightKnight", "Horse", "Boss" /* Add more tags as needed */ };

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

    public void SetStatsBack()
    {
        Debug.Log("Stats are set back");
        foreach(GameObject powerup in powerupDisplayRef.powerupsInGame)
        {
            PowerupStats powerupStats = powerup.GetComponent<PowerupStats>();

            powerupStats.SetStartingStats();
        }
    }

    public void BossDied()
    {
            demoOverScreen.SetActive(true);
            StartCoroutine(GoNow());
    }

    public IEnumerator GoNow()
    {
        yield return new WaitForSeconds(10);
        demoOverScreen.SetActive(false);
        youCanLeaveNowScreen.SetActive(true);
    }
}


