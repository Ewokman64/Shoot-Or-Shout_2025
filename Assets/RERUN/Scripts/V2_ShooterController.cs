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
}