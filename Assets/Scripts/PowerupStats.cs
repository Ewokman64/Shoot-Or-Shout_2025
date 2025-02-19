using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupStats : MonoBehaviour
{
    public string powerupName;
    public string powerupDescription;

    public float currentStat; //The stat the player has during the run.
    public string unit;
    public string currentStatDesc;

    public float improveAmount;

    public bool isEquipped;

    public float originalStat; //The stat the player starts out with. Need it so we can set it back at gamestart.
    // Start is called before the first frame update
    void Start()
    {
        SetStartingStats();      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetStartingStats()
    {
        currentStat = originalStat;
    }
}
