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
    public AudioClip shout;
    public ParticleSystem shoutEffect;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        shoutAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        TaunterMovement();
        Taunt();       
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
        if (Input.GetKeyDown(KeyCode.Q) && tauntCoolDown <= 0)
        {
            gameManager.isShooterChased = false;
            gameManager.isTaunterChased = true;
            tauntCoolDown = 2f;
            animator.SetTrigger("Taunt");
            shoutAudio.PlayOneShot(shout, 1.0f);
        }
        if (tauntCoolDown > 0)
        {
            tauntCoolDown -= Time.deltaTime;
        }
        else
        {
            tauntCoolDown = 0;
        }
        if (gameManager.isSomeoneDead == true)
        {
            shoutAudio.mute = true;
        }
    }
}
