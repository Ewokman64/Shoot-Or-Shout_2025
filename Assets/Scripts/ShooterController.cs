using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    private float speed = 10;
    private float yRange = 5;
    public GameObject bulletPrefab;
    public float bulletCoolDown = 0;
    private AudioSource gunAudio;
    private GameManager gameManager;
    public AudioClip gunShot;
    public ParticleSystem gunSmoke;
    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
        Vector3 bulletPos = GameObject.FindGameObjectWithTag("BulletSpawnPoint").transform.position;
        Instantiate(bulletPrefab, bulletPos, bulletPrefab.transform.rotation);     
    }
    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.W) && bulletCoolDown <= 0)
        {
            gameManager.isShooterChased = true;
            gameManager.isTaunterChased = false;
            bulletCoolDown = 1f;
            BulletSpawn();
            gunAudio.PlayOneShot(gunShot, 1.0f);      
            gunSmoke.Play();
            
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
}
