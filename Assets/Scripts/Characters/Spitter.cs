using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spitter : MonoBehaviour
{
    public GameObject acidBall;
    public float fireRate = 3;
    
    public Transform targetShooter;
    public Transform targetTaunter;

    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        GameObject shooterObject = GameObject.Find("Shooter(Clone)");
        if (shooterObject != null)
        {
            targetShooter = shooterObject.GetComponent<Transform>();
        }
        GameObject taunterObject = GameObject.Find("Taunter(Clone)");
        if (taunterObject != null)
        {
            targetTaunter = taunterObject.GetComponent<Transform>();
        }
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine("ShootAcidBall");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isShooterChased == true && targetShooter != null)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public IEnumerator ShootAcidBall()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Instantiate(acidBall, transform.position, acidBall.transform.rotation);
            yield return new WaitForSeconds(fireRate);
        }      
    }
}
