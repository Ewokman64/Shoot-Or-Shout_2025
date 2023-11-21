using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed = 30;
    public int zombieValue = 3;
    public bool isZombieShot = false;
    private GameManager gameManager;
    private AudioManager audioManager;

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
        if (other.gameObject.CompareTag("Zombie"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject, 0.2f);
            isZombieShot = true;
            audioManager.PlayZombieDeath();
            gameManager.UpdateNormalCurrency(zombieValue);
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
