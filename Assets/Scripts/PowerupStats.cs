using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupStats : MonoBehaviour
{
    public string powerupName;
    public string powerupDescription;

    public float originalStat; //The stat the player starts out with

    public float currentStat; //The stat the player currently has

    public float LVL1Stat;
    public float LVL2Stat;
    public float LVL3Stat;

    public string currentLVL;

    public string nextLVL;

    public bool isEquipped;


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
