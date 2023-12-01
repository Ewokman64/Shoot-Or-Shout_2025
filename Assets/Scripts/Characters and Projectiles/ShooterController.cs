using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public List<string> hostile;
    //Movement
    private float speed = 10;
    private float yRange = 5;
    //Bullet and light
    public GameObject bulletPrefab;
    public GameObject powerUpLight;
    private bool isPowerUpActive = false;
    public float bulletCoolDown = 0;

    private AudioSource gunAudio;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private CountDown countDown;

    //Anim, SFX & VFX
    private AudioManager audioManager;
    public AudioClip gunShot;
    public AudioClip powerUpSFX;
    public ParticleSystem gunFlash1;
    public ParticleSystem gunFlash2;
    public Animator animator;
  
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
        DualShoot();
        BulletCooldown();
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
        if (Input.GetKeyDown(KeyCode.W) && bulletCoolDown <= 0 && gameManager.isSomeoneDead == false)
        {
            gameManager.isShooterChased = true;
            gameManager.isTaunterChased = false;

            bulletCoolDown = 1f;
            BulletSpawn();

            audioManager.PlayShoot();
            gunFlash1.Play();
        }      
        /*if (gameManager.isSomeoneDead == true)
        {
            gunAudio.mute = true;
        }*/
    }

    public void DualShoot()
    {
        if (Input.GetKeyDown(KeyCode.W) && isPowerUpActive == true && gameManager.isSomeoneDead == false)
        {
            gameManager.isShooterChased = true;
            gameManager.isTaunterChased = false;

            DualBulletSpawn();

            gunAudio.PlayOneShot(gunShot, 1.0f);
            gunFlash1.Play();
            gunFlash2.Play();
        }
    }

    public void BulletCooldown()
    {
        if (bulletCoolDown > 0)
        {
            bulletCoolDown -= Time.deltaTime;
        }
        else
        {
            bulletCoolDown = 0;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            ActivatePowerUp();
        }
        else if (hostile.Contains(other.tag))
        {      
            gameManager.isSomeoneDead = true;
            gameManager.GameOver();
        }
        
    }
    public void ActivatePowerUp()
    {
        countDown.powerUpCanvas.gameObject.SetActive(true);
        powerUpLight.gameObject.SetActive(true);

        StartCoroutine(countDown.CountDownPowerUp());

        isPowerUpActive = true;
        //Effects and anims
        gunAudio.PlayOneShot(powerUpSFX, 1.0f);
        animator.SetBool("IsPowerUpActive", true);
        //Powerup over
        Invoke(nameof(SetBoolBack), 5.0f);
    }
    public void SetBoolBack()
    {
        animator.SetBool("IsPowerUpActive", false);
        isPowerUpActive = false;
        powerUpLight.gameObject.SetActive(false);
        spawnManager.powerUps = 0;
    }
}
