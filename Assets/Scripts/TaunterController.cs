using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaunterController : MonoBehaviour
{
    public List<string> hostile;

    [Header("Shout Settings")]
    public float tauntCoolDown;
    public float tauntCDRate = 2;
    public SpriteRenderer shouterCDRenderer; //<- This is for the Shout ICON on the bottom.
    public float darkenAmount = 0.5f; // Value between 0 and 1
    private Color originalColor;

    [Header("Dash Shout Properties")]
    public GameObject poweredShout;
    private CharacterMovement shouter_Mov_Ref;
    private CharacterMovement shooter_Mov_Ref;
    private Color enemyColor;
    private Color enemyFrozenColor;
    public bool isDashShoutUnlocked = false;


    private AudioSource shoutAudio;
    private GameManager gameManager;
    private AudioManager audioManager;
    private CountDown countDown;

    //Anims, VFX & SFX
    public AudioClip shout;
    public ParticleSystem shoutEffect;
    public Animator animator;

    public SpriteRenderer spriteRenderer;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        shoutAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        countDown = gameManager.GetComponent<CountDown>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        shouterCDRenderer = GameObject.Find("ShoutCDSprite").GetComponent<SpriteRenderer>();
        // Get the current color of the sprite
        originalColor = shouterCDRenderer.color;

        shouter_Mov_Ref = GetComponent<CharacterMovement>();
        shooter_Mov_Ref = GameObject.Find("Shooter(Clone)").GetComponent<CharacterMovement>();

        defaultColor = spriteRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        Taunt();
        TauntCoolDown();
        DashShout();
    }
    public void Taunt()
    {
        if (Input.GetKeyDown(KeyCode.Q) && tauntCoolDown <= 0 && Time.timeScale != 0)
        {
            gameManager.isShooterChased = false;
            gameManager.isTaunterChased = true;
            tauntCoolDown = tauntCDRate;
            
            audioManager.PlayShout();
            animator.SetTrigger("Taunt");
        }        
    }
    public void DashShout()
    {
        if (Input.GetKeyDown(KeyCode.Q) && shouter_Mov_Ref.isDashPowerOn && isDashShoutUnlocked)
        {
            shouter_Mov_Ref.isDashPowerOn = false;
            shooter_Mov_Ref.isDashPowerOn= false;

            gameManager.isShooterChased = false;
            gameManager.isTaunterChased = true;

            audioManager.PlayDashShout();
            StartCoroutine(ShoutVFX());
            StartCoroutine(SlowEnemies());
        }
    }
    public void TauntCoolDown()
    {
        if (tauntCoolDown > 0)
        {
            tauntCoolDown -= Time.deltaTime;
            // Darken the color by multiplying it with a darker shade
            Color darkenedColor = originalColor * darkenAmount;

            // Set the darkened color to the sprite
            shouterCDRenderer.color = darkenedColor;
        }
        else
        {
            tauntCoolDown = 0;
            shouterCDRenderer.color = originalColor;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {      
        if (hostile.Contains(other.tag))
        {
            UseShield useShieldRef = GetComponent<UseShield>();
            if (useShieldRef.shield.activeSelf == false)
            {
                gameManager.playerHealth--;
                gameManager.playerHealthText.text = "Health: " + gameManager.playerHealth;

                StartCoroutine(DamageColor());
                audioManager.PlayDamaged();

                if (gameManager.playerHealth <= 0)
                {
                    Destroy(gameObject);
                    gameManager.isSomeoneDead = true;
                    gameManager.GameOver();
                }
            }           
        }
        Destroy(other.gameObject);
    }

    public IEnumerator ShoutVFX()
    {
        poweredShout.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        poweredShout.SetActive(false);
    }

    public IEnumerator SlowEnemies()
    {
        //Access the spawnmanager's list called Enemies
        SpawnEnemies spawnEnemiesRef = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();

        foreach (GameObject enemies in spawnEnemiesRef.enemies)
        {
            EnemyStats enemyStats = enemies.GetComponent<EnemyStats>();

            SpriteRenderer enemySpriteRenderer = GetComponent<SpriteRenderer>();

            Debug.Log(enemies.name);

            enemyColor = enemySpriteRenderer.color;

            enemyFrozenColor = enemySpriteRenderer.color;

            enemyStats.movementSpeed *= 0.5f;           
        }

        yield return new WaitForSeconds(3);

        foreach (GameObject enemies in spawnEnemiesRef.enemies)
        {
            EnemyStats enemyStats = enemies.GetComponent<EnemyStats>();

            Debug.Log(enemies.name);

            enemyStats.movementSpeed = enemyStats.defaultMovSpeed;
        }        
    }

    public IEnumerator DamageColor()
    {
        yield return null;
        spriteRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        spriteRenderer.material.color = defaultColor;
    }
}
