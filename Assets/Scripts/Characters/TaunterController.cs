using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaunterController : MonoBehaviour
{
    public List<string> hostile;
    public float speed = 10;
    private readonly float yRange = 5;

    public float tauntCoolDown;
    public float tauntCDRate = 2;

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
    }

    // Update is called once per frame
    void Update()
    {
        TaunterMovement();
        Taunt();
        TauntCoolDown();
    }
    void TaunterMovement()
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
        }
        else
        {
            tauntCoolDown = 0;
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {      
        if (hostile.Contains(other.tag))
        {
            Destroy(gameObject);
            gameManager.isSomeoneDead = true;
            gameManager.GameOver();
        }
        Destroy(other.gameObject);
    }
}
