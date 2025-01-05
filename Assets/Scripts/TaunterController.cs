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

    private AudioSource shoutAudio;
    private GameManager gameManager;
    private AudioManager audioManager;
    private CountDown countDown;

    //Anims, VFX & SFX
    public AudioClip shout;
    public ParticleSystem shoutEffect;
    public Animator animator;

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
    }

    // Update is called once per frame
    void Update()
    {
        Taunt();
        TauntCoolDown();
    }
    public void Taunt()
    {
        if (Input.GetKeyDown(KeyCode.Q) && tauntCoolDown <= 0 && !gameManager.isSomeoneDead && Time.timeScale != 0)
        {
            gameManager.isShooterChased = false;
            gameManager.isTaunterChased = true;
            tauntCoolDown = tauntCDRate;
            
            audioManager.PlayShout();
            animator.SetTrigger("Taunt");
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
}
