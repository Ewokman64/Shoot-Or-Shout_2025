using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    private float speed = 10;
    private float yRange = 5;
    public GameObject bulletPrefab;
    public GameObject powerUpLight;
    public float bulletCoolDown = 0;
    private AudioSource gunAudio;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private CountDown countDown;
    private AudioManager audioManager;
    public AudioClip gunShot;
    public AudioClip powerUpSFX;
    public ParticleSystem gunFlash1;
    public ParticleSystem gunFlash2;
    public Animator animator;
    private bool isPowerUpActive = false;
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        countDown = GameObject.Find("GameManager").GetComponent<CountDown>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void Update()
    {
        ShooterMovement();
        Shoot();
    }
    void ShooterMovement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
        }
    }
    void BulletSpawn()
    {
        Vector3 bulletPos1 = GameObject.FindGameObjectWithTag("BulletSpawnPoint").transform.position;
        Instantiate(bulletPrefab, bulletPos1, bulletPrefab.transform.rotation);
    }
    void DualBulletSpawn()
    {
        Vector3 bulletPos1 = GameObject.FindGameObjectWithTag("BulletSpawnPoint").transform.position;
        Vector3 bulletPos2 = GameObject.FindGameObjectWithTag("BulletSpawnPoint2").transform.position;
        Instantiate(bulletPrefab, bulletPos1, bulletPrefab.transform.rotation);
        Instantiate(bulletPrefab, bulletPos2, bulletPrefab.transform.rotation);
    }
    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.W) && bulletCoolDown <= 0)
        {
            gameManager.isShooterChased = true;
            gameManager.isTaunterChased = false;
            bulletCoolDown = 1f;
            BulletSpawn();
            audioManager.PlayShoot();
            gunFlash1.Play();
        }
        if (Input.GetKeyDown(KeyCode.W) && isPowerUpActive == true)
        {
            gameManager.isShooterChased = true;
            gameManager.isTaunterChased = false;
            DualBulletSpawn();
            gunAudio.PlayOneShot(gunShot, 1.0f);
            gunFlash1.Play();
            gunFlash2.Play();
        }
        if (bulletCoolDown > 0)
        {
            bulletCoolDown -= Time.deltaTime;
        }
        else
        {
            bulletCoolDown = 0;
        }
        if (gameManager.isSomeoneDead == true)
        {
            gunAudio.mute = true;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            countDown.powerUpCanvas.gameObject.SetActive(true);
            powerUpLight.gameObject.SetActive(true);
            StartCoroutine(countDown.CountDownPowerUp());
            animator.SetBool("IsPowerUpActive", true);
            isPowerUpActive = true;
            gunAudio.PlayOneShot(powerUpSFX, 1.0f);
            Destroy(other.gameObject);
            Invoke(nameof(SetBoolBack), 5.0f);
        }
    }
    public void SetBoolBack()
    {
        animator.SetBool("IsPowerUpActive", false);
        isPowerUpActive = false;
        powerUpLight.gameObject.SetActive(false);
        spawnManager.powerUps = 0;
    }
}
