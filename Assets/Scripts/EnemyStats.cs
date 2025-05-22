using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string name; //we can check the name for collision?
    
    public float currentSpeed;
    public float regularSpeed;
    public float timeSlowSpeed;

    public float health;
    public float rateOfFire;
    public int points;
    public int currencyValue;

    private void Start()
    {
        //movementSpeed = defaultMovSpeed;
    }
}
