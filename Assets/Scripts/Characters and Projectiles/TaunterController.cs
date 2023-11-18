using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaunterController : MonoBehaviour
{
    private readonly float speed = 10;
    private readonly float yRange = 5;

    public float tauntCoolDown;

    private AudioSource shoutAudio;
    private GameManager gameManager;
    private AudioManager audioManager;

    //Anims, VFX & SFX
    public AudioClip shout;
    public ParticleSystem shoutEffect;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        shoutAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        if (Input.GetKeyDown(KeyCode.Q) && tauntCoolDown <= 0 && gameManager.isSomeoneDead == false)
        {
            gameManager.isShooterChased = false;
            gameManager.isTaunterChased = true;
            tauntCoolDown = 2f;
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
}
