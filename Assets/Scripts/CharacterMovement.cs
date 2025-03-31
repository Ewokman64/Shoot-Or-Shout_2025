using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    AudioManager audioManager;
    //Contains the movement mechanics of Shooter and Shouter

    [Header("Movement Settings")]
    public float currentSpeed;
    private float yRange = 5;

    [Header("Dash Settings")]
    public float dashCoolDown = 0;
    public float dashCDRate = 2f;

    [Header("Dash Power Settings")]
    public bool isDashPowerOn;
    public GameObject upperDashEffect;
    public GameObject lowerDashEffect;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    private void Update()
    {
        Movement();
        DashCooldown();
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (Input.GetKeyDown(KeyCode.Space) && dashCoolDown <= 0)
            {
                // Move the object using the current speed
                transform.Translate(Vector2.up * Time.deltaTime * currentSpeed * 50);
                dashCoolDown = dashCDRate;
                isDashPowerOn = true;
                StartCoroutine(PlayDashEffect(lowerDashEffect));
            }

            // Move the object using the current speed
            transform.Translate(Vector2.up * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKeyDown(KeyCode.Space) && dashCoolDown <= 0)
            {
                // Move the object using the current speed
                transform.Translate(Vector2.down * Time.deltaTime * currentSpeed * 50);
                dashCoolDown = dashCDRate;
                isDashPowerOn = true;
                StartCoroutine(PlayDashEffect(upperDashEffect));
            }
            // Move the object using the current speed
            transform.Translate(Vector2.down * Time.deltaTime * currentSpeed);
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
    public void DashCooldown()
    {
        if (dashCoolDown > 0)
        {
            dashCoolDown -= Time.deltaTime;
        }
        else
        {
            dashCoolDown = 0;
        }
    }

    public IEnumerator PlayDashEffect(GameObject dashEffect)
    {
        dashEffect.SetActive(true);
        audioManager.PlayDashSound();
        yield return new WaitForSeconds(0.15f);
        dashEffect.SetActive(false);
    }
}
