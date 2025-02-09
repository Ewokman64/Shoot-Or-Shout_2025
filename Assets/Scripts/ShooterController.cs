using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public List<string> hostile;
    
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public GameObject powerUpLight;
    public Sprite singleShotSprite;
    public Sprite dualShotSprite;
    private bool isPowerUpActive = false;
    public float bulletCoolDown = 0;
    public float bulletCDRate = 1.5f;
    public SpriteRenderer shooterSpriteRenderer;
    public SpriteRenderer shooterCDRenderer;
    public float darkenAmount = 0.5f; // Value between 0 and 1
    private Color originalColor;

    private AudioSource gunAudio;
    private GameManager gameManager;
    private SpawnManager spawnManager;
    private CountDown countDown;


    [Header("Anim,SFX,VFX")]
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
        shooterSpriteRenderer = GameObject.Find("Shooter(Clone)").GetComponent<SpriteRenderer>();
        shooterCDRenderer = GameObject.Find("ShootCDSprite").GetComponent<SpriteRenderer>();
        // Get the current color of the sprite
        originalColor = shooterCDRenderer.color;

    }

    public void Update()
    {
        Shoot();
        DualShoot();
        BulletCooldown();
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
        if (Input.GetKeyDown(KeyCode.W) && bulletCoolDown <= 0 && !gameManager.isSomeoneDead && Time.timeScale != 0 && !isPowerUpActive)
        {
            gameManager.isShooterChased = true;
            gameManager.isTaunterChased = false;

            bulletCoolDown = bulletCDRate;
            BulletSpawn();
            audioManager.PlayShoot();
            gunFlash1.Play();
        }
    }
    public void DualShoot()
    {
        if (Input.GetKeyDown(KeyCode.W) && isPowerUpActive == true && !gameManager.isSomeoneDead && Time.timeScale != 0)
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
            // Darken the color by multiplying it with a darker shade
            Color darkenedColor = originalColor * darkenAmount;

            // Set the darkened color to the sprite
            shooterCDRenderer.color = darkenedColor;
        }
        else
        {
            bulletCoolDown = 0;
            shooterCDRenderer.color = originalColor;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            //ActivateDualShotSprite
            shooterSpriteRenderer.sprite = dualShotSprite;
            ActivatePowerUp();
        }
        else if (hostile.Contains(other.tag))
        {
            gameManager.playerHealth--;
            gameManager.playerHealthText.text = "Health: " + gameManager.playerHealth;
            if (gameManager.playerHealth <= 0)
            {
                Destroy(gameObject);
                gameManager.isSomeoneDead = true;
                gameManager.GameOver();
            }
        }
        Destroy(other.gameObject);

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
        shooterSpriteRenderer.sprite = singleShotSprite;
        animator.SetBool("IsPowerUpActive", false);
        isPowerUpActive = false;
        powerUpLight.gameObject.SetActive(false);
        spawnManager.powerUps = 0;
    }
}
