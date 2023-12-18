using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 30;
    public int zombieValue = 3;
    public bool isZombieShot = false;
    private GameManager gameManager;
    private AudioManager audioManager;
    private NightKnight nightKnight;
    private Horse horse;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * bulletSpeed);
    }
    public void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("BigEnemy") || other.gameObject.CompareTag("Spitter"))
        {
            Destroy(other.gameObject, 0.2f);
            isZombieShot = true;
            audioManager.PlayZombieDeath();
            gameManager.UpdateNormalCurrency(zombieValue);
        }
        else if (other.gameObject.CompareTag("Shield"))
        {
            nightKnight = GameObject.Find("NightKnight(Clone)").GetComponent<NightKnight>();
            nightKnight.shieldHealth--;
            if (nightKnight.shieldHealth <= 0)
            {
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("NightKnight"))
        {
            nightKnight = GameObject.Find("NightKnight(Clone)").GetComponent<NightKnight>();
            nightKnight.nightKnightHealth--;
        }
        else if (other.gameObject.CompareTag("Horse"))
        {
            horse = GameObject.Find("Horse").GetComponent<Horse>();
            horse.horseHealth--;
        }
        if (isZombieShot == true)
        {
            Invoke(nameof(SetBoolBack), 1.0f);
        }
        Destroy(gameObject);
    }
    private void SetBoolBack()
    {
        isZombieShot = false;
    }

}
