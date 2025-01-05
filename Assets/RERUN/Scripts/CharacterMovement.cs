using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    //Contains the movement mechanics of Shooter and Shouter

    [Header("Movement Settings")]
    public float currentSpeed = 0;
    public float baseSpeed = 5;
    public float maxSpeed = 10;
    public float accelerationRate = 1.0f;
    private float yRange = 5;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed = 10;
    [SerializeField] float dashDuration = 1;
    [SerializeField] float dashCooldown = 1;
    [SerializeField] float dashCDRate = 1.5f;
    [SerializeField] bool isDashing;

    private void Update()
    {
        Movement();
        Dash();
        DashCoolDown();
    }
    void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Accelerate();

            // Move the object using the current speed
            transform.Translate(Vector2.up * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Accelerate();

            // Move the object using the current speed
            transform.Translate(Vector2.down * Time.deltaTime * currentSpeed);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) ||  Input.GetKeyUp(KeyCode.DownArrow))
        {
            currentSpeed = baseSpeed;
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
    void Accelerate()
    {
        //Increases the current speed by the accelerationRate overtime
        currentSpeed = currentSpeed + accelerationRate * Time.deltaTime;

        //Clamp the speed to a max limit
        currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dashCooldown <= 0)
        {
            StartCoroutine(SetDashSpeed());
        }
    }

    public IEnumerator SetDashSpeed()
    {
        //Changing maxSpeed so the dash feels actually fast
        maxSpeed = 50;
        currentSpeed += dashSpeed;

        //Puts the dash on Cooldown
        dashCooldown = dashCDRate;

        //Duration of the dash is set to short, so you don't dash out of the galaxy
        yield return new WaitForSeconds(dashDuration);

        //Max speed is set back so we don't accelerate too much
        maxSpeed = 10;

        //Setting the speed to default
        currentSpeed = baseSpeed;
    }

    public void DashCoolDown()
    {
        if (dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
        }
        else
        {
            dashCooldown = 0;
        }
    }
}
