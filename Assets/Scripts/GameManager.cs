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
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    void Update()
    {   
        scoreManager();
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

    public void ClearMap()
    {
        spawnManager.StopAllCoroutines();
        //GET RID OF BULLETS
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");

        foreach (GameObject bullet in bullets)
            Destroy(bullet);

        //GET RID OF ENEMIES
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
            Destroy(enemy);
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
    /*void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered!");
        if (collision.gameObject == shooter && collision.gameObject.CompareTag("Zombie"))
        {
            Debug.Log("Someone died!");
            isSomeoneDead = true;
            GameOver();
        }
    }*/
}


