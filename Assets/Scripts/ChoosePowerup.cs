using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePowerup : MonoBehaviour
{

    PowerUpDisplay powerupDisplay;
    SpawnEnemies waveManager;

    // Start is called before the first frame update
    void Start()
    {
        powerupDisplay = GameObject.Find("PowerupManager").GetComponent<PowerUpDisplay>();
        waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
    }

    // Update is called once per frame
    /*void Update()
    {
        PickPowerup();
    }

    void PickPowerup()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PowerupStats powerupStat = powerupDisplay.powerupsContainer[0].GetComponent<PowerupStats>();

            if (powerupStat.isEquipped)
            {
                powerupStat.UpdateLVL();
            }
            else
            {
                //Find a free slot and assign the powerups image to it
                powerupStat.isEquipped = true;
            }

            powerupDisplay.powerupButtons[0].onClick.Invoke();
            powerupDisplay.powerupPanel.SetActive(false);
            Time.timeScale = 1;
            waveManager.StartWaves();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PowerupStats powerupStat = powerupDisplay.powerupsContainer[1].GetComponent<PowerupStats>();
            powerupStat.isEquipped = true;

            powerupDisplay.powerupButtons[1].onClick.Invoke();
            powerupDisplay.powerupPanel.SetActive(false);
            Time.timeScale = 1;
            waveManager.StartWaves();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PowerupStats powerupStat = powerupDisplay.powerupsContainer[2].GetComponent<PowerupStats>();
            powerupStat.isEquipped = true;

            powerupDisplay.powerupButtons[2].onClick.Invoke();
            powerupDisplay.powerupPanel.SetActive(false);
            Time.timeScale = 1;
            waveManager.StartWaves();
        }
    }*/

    void CanPowerUpSpawn()
    {
        //Gonna check if there is enough space, put the powerup into the right inventory slot, etc.
        //0. Check if we already have the powerup. If yes, we UPGRADE our already equipped one. If not, we proceed to step 1.
        //1. Look through slots 1-5 and assign powerup to the first available slot
        //2. If there is no available slot, pop up message
    }
}
