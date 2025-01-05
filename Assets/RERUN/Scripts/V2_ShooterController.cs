using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2_ShooterController : MonoBehaviour
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public float bulletCoolDown = 0;
    public float bulletCDRate = 0.5f;

    private void Start()
    {
         
    }
    private void Update()
    {
        //ShooterMovement();
        Shoot();
        BulletCooldown();
    }
    /*void ShooterMovement()
    {
        moveUp = Vector2.up * Time.deltaTime * speed;
        moveDown = Vector2.down * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Maybe check here if a space is pressed as well?
            if (Input.GetKey(KeyCode.Space) && isDashing == false)
            {
                StartCoroutine(Dash(moveUp));
            }
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.Space) && isDashing == false)
            {
                StartCoroutine(Dash(moveDown));
            }
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
        }
    }*/

    void BulletSpawn()
    {
        Vector3 bulletPos = GameObject.FindGameObjectWithTag("BulletSpawnPoint").transform.position;
        Instantiate(bulletPrefab, bulletPos, bulletPrefab.transform.rotation);
    }

    public void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.W) && bulletCoolDown <= 0)
        {
            bulletCoolDown = bulletCDRate;
            BulletSpawn();
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
    /*public IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        transform.Translate(direction * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }*/
}