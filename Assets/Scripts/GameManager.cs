using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class GameManager : MonoBehaviour
{
    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private SpawnManager spawnManager;
    public WallHealthBar wallHealthBar;
    public GameManager gameManager;
    private UpgradeList upgradeList;
    private UpgradesManager upgradesManager;
    private CountDown countDown;
    public Bullet bulletPrefab; // Reference to the Bullet script attached to a prefab
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
    public bool hasGameStarted;
    public bool bossDied;
    public bool gameIsPaused = false;

    void Start()
    {
        Time.timeScale = 0;
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
        countDown = GetComponent<CountDown>();
        upgradeList.enabled = false;
        // Access the Bullet script without instantiating a visible GameObject
        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        bulletScript.SetHealth(0f);
    }

    void Update()
    {
        UpdateEnemyList();
        scoreManager();
        if (bossDied)
        {
            demoOverScreen.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        else if (upgradesManager.upgradePanel.activeSelf)
        {
                // Pause the game
                Debug.Log("Game Paused");
                gameIsPaused = true;
                Time.timeScale = 0;
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
    }
    public void AddEnemyToList(GameObject enemy)
    {
        spawnedEnemies.Add(enemy);
    }
    public void UpdateEnemyList()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            // Check if the enemy GameObject is null (destroyed)
            if (spawnedEnemies[i] == null)
            {
                // Enemy has been destroyed
                Debug.Log("Enemy " + i + " has been destroyed.");

                // Optionally, you can remove the destroyed enemy from the list
                spawnedEnemies.RemoveAt(i);
                //Spitter is not getting removed
            }
        }
    }
    public void RemovePointFromOccupied()
    {
        //IF the removed enemy is ranged, remove one transform from the occupied spawnlist
    }
    public void StartGame()
    {
        //"previousEasyScore" key is getting loaded"
        SpawnEnemies waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();  
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
        Destroy(GameObject.FindWithTag("Laser"));
        gmAudio.mute = true;
        ClearMap();
        spawnManager.StopAllCoroutines();
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
            // Pause the game
            Debug.Log("Game Paused");
            Time.timeScale = 0;
            gameIsPaused = true;
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
        string[] enemyTags = { "Enemy", "BigEnemy", "Spitter", "NightKnight", "Horse", "FinalPush", "Boss", "Projectile" /* Add more tags as needed */ };

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


