using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopList : MonoBehaviour
{
    public GameManager gameManager;
    public WallHealthBar wallHealthBar;

    int maxPlayerHealth = 7;
    int maxWallHealth = 30;
    int maxScoreMulti = 5;

    int playerHealth_upgradeAmount = 1;
    int wallHealth_upgradeAmount = 5;
    int maxScoreMulti_upgradeAmount = 1;

    public void MaxHealth()
    {
        if (gameManager.playerHealth < maxPlayerHealth)
        {
            gameManager.playerHealth += playerHealth_upgradeAmount;
        } 
    }
    public void WallHealth()
    {
        if (wallHealthBar.maxHealth < maxWallHealth)
        {
            wallHealthBar.maxHealth += wallHealth_upgradeAmount;
        }
    }

    public void ScoreMultiplier()
    {
        if (gameManager.scoreMultiplier < maxScoreMulti)
        {
            gameManager.scoreMultiplier += maxScoreMulti_upgradeAmount;
        }
    }
}
