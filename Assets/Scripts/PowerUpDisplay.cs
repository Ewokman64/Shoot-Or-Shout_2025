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
    public List<GameObject> powerupsInGame; //This list contains every selectable, run only powerups that Shoot or Shout has.

    public List<GameObject> powerupsChosen; //This list gonna contain the 3 powerups the shuffling function has chosen.

    public List<GameObject> offeredContainer; //Get the Image and Text components of this 3 empty

    public List<Button> upgradeButtons; //contains the button components we get from instantiated upgrades so we can access and call them via key presses
    public List<GameObject> powerupsEquipped; //This list gonna contain every powerup the player successfully equips
    public GameObject powerupPanel;

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
        upgradeButtons.Clear(); //We can clear the buttons as well, possibly for overlapping

        GetRandomPowerUps();

        //After we randomized the 3 powerups, we need to call the DisplayPowerUps
        DisplayPowerUps(powerupsChosen, powerupPanel);

        powerupPanel.SetActive(true);
        //I think I woN't use clickable buttons, only the keys 1-2-3
        //Maybe we can also add the option to delete powerups with other keys to free up space. Maybe Q-W-E-R-T? Or CTRL+1-2-3-4-5
        //Psuedo goes something like "if key1 is pressed, equip the first item on the list, and so on"
    }

    //In this function we delete the powerupinstances so they don't overlap eachother each time we call display.
    public void DeletePowerupInstances()
    {
        foreach (GameObject obj in powerupsChosen)
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
    //It requires: a list<GameObject> containing the chosen powerups, another list that contains the 3 empty slots gameobjects in the UI, and a GameObject (our UI) to function
    public void DisplayPowerUps(List<GameObject> chosenList, GameObject powerupPanel)
    {
        // Loop through each powerup and assign the position of each empty slot
        for (int i = 0; i < Mathf.Min(chosenList.Count); i++)
        {
            // Get the empty slot position directly. Emptyslots represents the empty container that can take 3 visible powerups
            Image placeholderImage = offeredContainer[i].GetComponent<Image>();

            Image powerupImage = powerupsChosen[i].GetComponent<Image>();

            placeholderImage.sprite = powerupImage.sprite;
        }
    }
}
