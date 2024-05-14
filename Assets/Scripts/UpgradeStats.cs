using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeStats : MonoBehaviour
{
    public string upgradeName;
    public string upgradeDescription;
    public string currentStatString;
    public float upgradeAmount;
    public float currentStat;
    public string currentStatDescription;
    public int LVL = 1;
    public int upgradeSlot;
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
        currentStat = upgradeAmount * LVL; //example BulletPierceStat: 1 * 1 (LVL) = 1
        currentStatString = currentStatDescription + " " + currentStat.ToString(); 
    }

    public void SetStartingStats()
    {
        currentStat = upgradeAmount;
    }
}
