using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string name; //we can check the name for collision?
    
    public float currentSpeed;
    public float regularSpeed;
    public float slowSpeed;

    public string movementType;

    public float health;
    public float rateOfFire;
    public int points;
    public int currencyValue;

    public GameObject projectile;

    private void Start()
    {
        currentSpeed = regularSpeed;
        slowSpeed = regularSpeed * 0.5f;
    }
}
