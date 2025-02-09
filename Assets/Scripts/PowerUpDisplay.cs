using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PowerUpDisplay : MonoBehaviour
{
    [Header("Powerup collection")]
    public List<GameObject> powerupsInGame; //This list contains every selectable, run only powerups that Shoot or Shout has.

    [Header("Randomly picked powerups")]
    public List<GameObject> powerupsChosen; //This list gonna contain the 3 powerups the shuffling function has chosen and will be displayed
    public List<GameObject> powerupInstances; //Creating a temporary list for the powerup instances. We need this so we can destroy them each time a display happens. Helps to avoid overlapping powerups.

    [Header("Containers")]
    public List<Transform> powerupsContainer; //This is where we instantiate our powerups to
    public List<GameObject> powerupsInventory; //This list gonna contain every powerup the player already equipped

    [Header("Other")]
    public List<Button> powerupButtons; //Contains the button components of the 3 chosen powerup
    public List<PowerupStats> powerupStats; //Need it to check whether a powerup was picked or not in ChoosePowerup.cs

    public GameObject powerupPanel; //The UI panel that contains the visuals

    SpawnEnemies waveManager; //We access the wavemanager to stop and restart wave spawning after a powerup got picked
    void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
    }
    //Within this function we gonna clear the scene as much as possible, then run our DisplayPowerUp function + we add the corresponding 1-2-3 keys so we can choose too
    public void OfferPowerUps()
    {
        waveManager.StopAllCoroutines(); //We stop all the waves from spawning

        DeletePowerupInstances(); //We delete the powerupinstances so they don't overlap

        GetRandomPowerUps();
        DisplayPowerUps(powerupsChosen, powerupPanel); //After we randomized the 3 powerups, we need to call the DisplayPowerUps
        GetButtons(); // We access the buttons so they can be invoked in ChoosePowerup.cs
        SetPowerupText();
        powerupPanel.SetActive(true);


        Time.timeScale = 0;
    }

    //In this function we delete the powerup instances so they don't overlap eachother each time we call display.
    public void DeletePowerupInstances()
    {
        foreach (GameObject obj in powerupInstances)
        {
            // Destroy each instantiated object
            Destroy(obj);
        }
    }

    public void GetRandomPowerUps()
    {
        powerupsInGame.Shuffle(); //We shuffle the list that contains all the powerups in Shoot or Shout.

        //Then we select the first 'i' powerup from the shuffled list and add it to this local list called randomPowerups. We repeat it for all 3.
        for (int i = 0; i < 3; i++)
        {
            powerupsChosen.Add(powerupsInGame[i]);
        }

        //By the end of this function, all 3 powerups are successfully chosen randomly//
    }


    //This function instantiates the 3 chosen powerups and parents them to the powerup panel. 
    //It requires: a list<GameObject> containing the chosen powerups, and a GameObject to function
    public void DisplayPowerUps(List<GameObject> chosenList, GameObject powerupPanel)
    {
        // Loop through each powerup and assign the position of each empty slot
        for (int i = 0; i < Mathf.Min(chosenList.Count, powerupsContainer.Count); i++)
        {
            // Use the corresponding spawn point for each powerup
            Transform spawnPoint = powerupsContainer[i].transform;

            GameObject powerupInstance = Instantiate(chosenList[i], spawnPoint.position, Quaternion.identity);

            powerupInstances.Add(powerupInstance);
            // Set the instantiated powerup as a child of the powerup panel
            powerupInstance.transform.SetParent(powerupPanel.transform);
        }
    }

    public void GetButtons()
    {
        for (int i = 0; i < Mathf.Min(powerupsChosen.Count); i++)
        {
            Button powerupButton = powerupsChosen[i].GetComponent<Button>();

            powerupButtons.Add(powerupButton);
        }
    }

    public void SetPowerupText()
    {

        for (int i = 0; i < 3; i++)
        {
            TextComponents textComponents = powerupsContainer[i].GetComponent<TextComponents>();
            PowerupStats powerupStats = powerupsChosen[i].GetComponent<PowerupStats>();

            textComponents.nameText.text = powerupStats.powerupName;
            textComponents.descText.text = powerupStats.powerupDescription;
            textComponents.statText.text = powerupStats.currentStatDesc + " " + powerupStats.currentStat + powerupStats.unit;
            //textComponents.lvlText.text = "Stat at next level: " + powerupStats.statNext;
        }
    }
}

