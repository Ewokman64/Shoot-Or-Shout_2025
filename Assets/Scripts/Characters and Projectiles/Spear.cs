using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour
{
    private float spearSpeed = 15;
    private GameManager gameManager;
    public bool isShooterTargeted = true;
    public bool isTaunterTargeted;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (gameManager.isShooterChased == true)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (gameManager.isTaunterChased == true)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isShooterChased == true)
        {
            isShooterTargeted = true;
            isTaunterTargeted = false;
        }
        if (gameManager.isTaunterChased == true)
        {
            isTaunterTargeted = true;
            isShooterTargeted = false;
        }
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * spearSpeed);
        }
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.Translate(Vector3.right * Time.deltaTime * spearSpeed);
        }
    }
}
