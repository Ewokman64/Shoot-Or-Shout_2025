using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 30;
    public int easyZombieValue = 1;
    public int normalZombieValue = 3;
    public int hardZombieValue = 6;
    public bool isZombieShot = false;
    private GameManager gameManager;
    private AudioManager audioManager;
    private DifficultyManager difficultyManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        difficultyManager = GameObject.Find("DifficultyManager").GetComponent<DifficultyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //This part is for the player
        if (other.gameObject.CompareTag("Zombie") && difficultyManager.easyMode == true)
        {   
            Destroy(gameObject);  
            Destroy(other.gameObject, 0.2f);
            isZombieShot = true;
            audioManager.PlayZombieDeath();
            gameManager.UpdateEasyCurrency(easyZombieValue);
        }
        if (other.gameObject.CompareTag("Zombie") && difficultyManager.normalMode == true)
        {
            Destroy(gameObject);
            Destroy(other.gameObject, 0.2f);
            isZombieShot = true;
            audioManager.PlayZombieDeath();
            gameManager.UpdateNormalCurrency(normalZombieValue);
        }
        if (other.gameObject.CompareTag("Zombie") && difficultyManager.hardMode == true)
        {
            Destroy(gameObject);
            Destroy(other.gameObject, 0.2f);
            isZombieShot = true;
            audioManager.PlayZombieDeath();
            gameManager.UpdateHardCurrency(hardZombieValue);
        }
        if (isZombieShot == true)
        {
            Invoke(nameof(SetBoolBack), 1.0f);
        }
            if (other.gameObject.CompareTag("Detector"))
        {
            Destroy(gameObject);
        }
    }
    private void SetBoolBack()
    {
        isZombieShot = false;
    }

}
