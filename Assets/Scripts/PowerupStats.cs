using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PowerupStats : MonoBehaviour
{
    public string powerupName;
    public string powerupDescription;
    public string currentStatString;
    public float powerupAmount;
    public float currentStat;
    public string currentStatDescription;
    public int LVL = 1;
    public int powerupSlot;

    public bool isEquipped;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLVL()
    {
        LVL++; //LVL -> 2
        currentStat = powerupAmount * LVL; //example BulletPierceStat: 1 * 1 (LVL) = 1
        currentStatString = currentStatDescription + " " + currentStat.ToString(); 
    }

    public void SetStartingStats()
    {
        currentStat = powerupAmount;
    }
}
