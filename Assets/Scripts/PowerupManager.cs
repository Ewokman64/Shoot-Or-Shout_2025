using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
    public List<GameObject> upgradePrefabs; // List of every upgrade in the game

    public List<GameObject> upgradesContainer; // The 3 empty containers where upgrades are displayed
    
    //*GENERATING AND SPAWNING UPGRADES
    public List<GameObject> selectedUpgrades; // We load the randomly generated upgrades into this list
    public List<GameObject> upgradeInstances; // I need this to destroy the upgrades once we pick one
    public List<Transform> upgradeSpawnPoints; // List of spawn points for displaying powerups
    //the instantiated upgrades are getting assigned to these GameObjects and their Button Components to these buttons

    public GameObject upgrade1;
    public GameObject upgrade2;
    public GameObject upgrade3;

    public Button button1;
    public Button button2;
    public Button button3;
    
    public List<Button> upgradeButtons; //contains the button components we get from instantiated upgrades so we can access and call them via key presses
    public GameObject powerupPanel; // Reference to the UI panel containing GameObject components
    SpawnEnemies waveManager; //We access the wavemanager to stop and restart wave spawning after an upgrade got picked

    public bool upgradeWasPicked = false;

    //*ADDING SELECTED UPGRADES TO OUR INVENTORY
    public List<Transform> equippedUpgradeSpawnPoints; // List of spawn points for displaying equipped powerups
    public List<string> equippedUpgradesNames;
    public List<GameObject> equippedUpgrades; //checking wether we have certain upgrades equipped or not
    public List<GameObject> equippedUpgradesContainer; // List of upgrade containers where EQUIPPED upgrades will be displayed. looping through them to get an available one
    bool isFreeSlotAvailable = true;

    public TextMeshProUGUI noSlotText;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
    }

    // Update is called once per frame
    void Update()
    {
        //ButtonPick();
        
    }
    void CheckEquipped()
    {
        if (equippedUpgrades.Count == 5)
        {
            isFreeSlotAvailable = false;
        }
    }
    public void DeleteUpgradeInstances()
    {
        foreach (GameObject obj in upgradeInstances)
        {
            // Destroy each instantiated object
            Destroy(obj);
        }
    }
    public void OfferPowerups()
    {
        
        waveManager.StopAllCoroutines();
        DeleteUpgradeInstances();
        upgradeButtons.Clear();
        //wasUpgradeChosen = false;
        // Update the last threshold to avoid repeated triggering
        //lastScoreThreshold = currentScoreThreshold;

        // Randomly select three unique powerups
        //selectedUpgrades = GetRandomUpgrades(3);
        upgrade1 = null;
        upgrade2 = null;
        upgrade3 = null;

        upgrade1 = selectedUpgrades[0];
        upgrade2 = selectedUpgrades[1];
        upgrade3 = selectedUpgrades[2];

        button1 = upgrade1.GetComponent<Button>();
        button2 = upgrade2.GetComponent<Button>();
        button3 = upgrade3.GetComponent<Button>();

        upgradeButtons.Add(button1);
        upgradeButtons.Add(button2);
        upgradeButtons.Add(button3);


        // Set the powerup panel active
        powerupPanel.SetActive(true);

        // Display the powerups to the player using the UI panel
        //DisplayUpgrades(selectedUpgrades, powerupPanel);
        //UpdateUpgradeText();
    }
    /*public void ButtonPick()
    {
        
        //**REWRITE SOMETIME
        //Psudeo: if there is freeslot available and if upgrade isn't part of are list, equip upgrade. if it's part of it, LVLUP upgrade
        //If there is no free slot available and upgrade isn't part of our list, says "no free slot left". if it's part of it, LVLUP upgrade
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (button1 != null)
            {
                CheckEquipped();
                //if (isFreeSlotAvailable)
                //{
                    // Simulate a button click
                    button1.onClick.Invoke();
                    waveManager.StartWaves();

                    //getting the availability of each container, looping through them
                    for (int i = 0; i < equippedUpgradesContainer.Count; i++)
                    {
                        
                        Availability availabilityScript = equippedUpgradesContainer[i].GetComponent<Availability>();

                        //checking for the first one that has IsAvailable on true

                        //if there is freeslot available AND if upgrade isn't part of our list, equip upgrade
                        if (availabilityScript.IsAvailable && !equippedUpgrades.Contains(upgrade1)) 
                        {
                            Debug.Log("This is a new upgrade, equpping....");
                            // Add the upgrade to the list
                            equippedUpgrades.Add(upgrade1);
                            // Get the Image components
                            Image equippedImage = equippedUpgradesContainer[i].GetComponent<Image>();
                            Image upgradeImage = upgrade1.GetComponent<Image>();
                            // Set the slot's image to the upgrade's image
                            equippedImage.sprite = upgradeImage.sprite;

                            //SETTING THE TEXT OF SLOT
                            //accessing text of emptycontainer and string from upgrade
                            TextMeshProUGUI emptyContainerText = equippedUpgradesContainer[i].GetComponentInChildren<TextMeshProUGUI>();
                            //accessing the currentStat string
                            PowerupStats powerupStats = upgrade1.GetComponent<PowerupStats>();
                            powerupStats.SetStartingStats();
                            //setting the emptycontainer text to the currentStat
                            emptyContainerText.text = powerupStats.currentStatString;

                            availabilityScript.IsAvailable = false;
                            availabilityScript.assignedUpgrade = upgrade1.name;
                            // Exit the loop after finding the first available slot
                            return;
                        }
                        //If there is no free slot available AND if upgrade IS part of our list, LVLUP the upgrade
                        else if (!availabilityScript.IsAvailable && equippedUpgrades.Contains(upgrade1))
                        {
                            //looks through all the upgrades we already have equipped
                            for (int j = 0; j < equippedUpgrades.Count; j++)
                            {
                                //checks if upgrade's name matches any of the assignedUpgrade(names) we have in this equipped list
                                if (upgrade1.name == availabilityScript.assignedUpgrade)
                                {
                                    Debug.Log("Upgrade is already in equipped");
                                    //accessing the currentStat string
                                    PowerupStats powerupStats = upgrade1.GetComponent<PowerupStats>();
                                    powerupStats.UpdateLVL();
                                    //SETTING THE TEXT OF SLOT
                                    //accessing text of emptycontainer and string from upgrade
                                    TextMeshProUGUI emptyContainerText = equippedUpgradesContainer[i].GetComponentInChildren<TextMeshProUGUI>();

                                    //setting the emptycontainer text to the currentStat
                                    emptyContainerText.text = powerupStats.currentStatString;

                                    availabilityScript.IsAvailable = false;

                                    // Exit the loop after finding the first available slot
                                    return;
                                }
                            }  
                        }
                        else if (!availabilityScript.IsAvailable && !equippedUpgrades.Contains(upgrade1))
                        {
                        noSlotText.text = "NO FREE SLOTS AVAILABLE FOR UNIQUE UPGRADE";
                        StartCoroutine(SetTextBack());
                        Debug.Log("No free slots available");
                        }
                    }
                    upgradeWasPicked = true;
                //}
            }
            else
            {
                Debug.LogError("Button 1 not assigned to the script.");
            }
        }
        // Check if a specific key is pressed (e.g., the space key)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (button2 != null)
            {
                CheckEquipped();
                //if (isFreeSlotAvailable)
                //{
                    // Simulate a button click
                    button2.onClick.Invoke();
                    waveManager.StartWaves();

                    for (int i = 0; i < equippedUpgradesContainer.Count; i++)
                    {
                        //getting the availability of each
                        Availability availabilityScript = equippedUpgradesContainer[i].GetComponent<Availability>();


                        //checking for the first one that has IsAvailable on true
                        if (availabilityScript.IsAvailable && !equippedUpgrades.Contains(upgrade2)) //and it doesn't contain a string name from the already equipped list
                        {
                            Debug.Log("This is a new upgrade, equpping....");
                            // Add the upgrade to the list
                            equippedUpgrades.Add(upgrade2);
                            // Get the Image components
                            Image equippedImage = equippedUpgradesContainer[i].GetComponent<Image>();
                            Image upgradeImage = upgrade2.GetComponent<Image>();
                            // Set the slot's image to the upgrade's image
                            equippedImage.sprite = upgradeImage.sprite;

                            //SETTING THE TEXT OF SLOT
                            //accessing text of emptycontainer and string from upgrade
                            TextMeshProUGUI emptyContainerText = equippedUpgradesContainer[i].GetComponentInChildren<TextMeshProUGUI>();
                            //accessing the currentStat string
                            PowerupStats powerupStats = upgrade2.GetComponent<PowerupStats>();
                            powerupStats.SetStartingStats();
                            //setting the emptycontainer text to the currentStat
                            emptyContainerText.text = powerupStats.currentStatString;

                            availabilityScript.IsAvailable = false;

                            // Exit the loop after finding the first available slot
                            return;
                        }
                        //if it contains the string name from the already equipped list, then overwrite the stats
                        else if (!availabilityScript.IsAvailable && equippedUpgrades.Contains(upgrade2))
                        {
                            //looks through all the upgrades we already have equipped
                            for (int j = 0; j < equippedUpgrades.Count; j++)
                            {
                                //checks if upgrade1's name matches any of the assignedUpgrade(names) we have in this equipped list
                                if (upgrade2.name == availabilityScript.assignedUpgrade)
                                {
                                    Debug.Log("Upgrade is already in equipped");
                                    //accessing the currentStat string
                                    PowerupStats powerupStats = upgrade2.GetComponent<PowerupStats>();
                                    powerupStats.UpdateLVL();
                                    //SETTING THE TEXT OF SLOT
                                    //accessing text of emptycontainer and string from upgrade
                                    TextMeshProUGUI emptyContainerText = equippedUpgradesContainer[i].GetComponentInChildren<TextMeshProUGUI>();

                                    //setting the emptycontainer text to the currentStat
                                    emptyContainerText.text = powerupStats.currentStatString;

                                    availabilityScript.IsAvailable = false;

                                    // Exit the loop after finding the first available slot
                                    return;
                                }
                            }
                        }
                        else if (!availabilityScript.IsAvailable && !equippedUpgrades.Contains(upgrade2))
                        {
                        noSlotText.text = "NO FREE SLOTS AVAILABLE FOR UNIQUE UPGRADE";
                        StartCoroutine(SetTextBack());
                        Debug.Log("No free slots available");
                        }
                    upgradeWasPicked = true;
                    }
                //}
            }
            else
            {
                Debug.LogError("Button 1 not assigned to the script.");
            }
        }
        // Check if a specific key is pressed (e.g., the space key)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (button3 != null)
            {
                CheckEquipped();
                //if (isFreeSlotAvailable)
                //{
                    // Simulate a button click
                    button3.onClick.Invoke();
                    waveManager.StartWaves();

                    for (int i = 0; i < equippedUpgradesContainer.Count; i++)
                    {
                        //getting the availability of each container
                        Availability availabilityScript = equippedUpgradesContainer[i].GetComponent<Availability>();


                        //checking for the first one that has IsAvailable on true
                        //if there is available space AND upgrade is not equipped
                        if (availabilityScript.IsAvailable && !equippedUpgrades.Contains(upgrade3)) //and it doesn't contain a string name from the already equipped list
                        {
                            Debug.Log("This is a new upgrade, equpping....");
                            // Add the upgrade to the list
                            equippedUpgrades.Add(upgrade3);
                            // Get the Image components
                            Image equippedImage = equippedUpgradesContainer[i].GetComponent<Image>();
                            Image upgradeImage = upgrade3.GetComponent<Image>();
                            // Set the slot's image to the upgrade's image
                            equippedImage.sprite = upgradeImage.sprite;

                            //SETTING THE TEXT OF SLOT
                            //accessing text of emptycontainer and string from upgrade
                            TextMeshProUGUI emptyContainerText = equippedUpgradesContainer[i].GetComponentInChildren<TextMeshProUGUI>();
                            //accessing the currentStat string
                            PowerupStats powerupStats = upgrade3.GetComponent<PowerupStats>();
                            powerupStats.SetStartingStats();
                            //setting the emptycontainer text to the currentStat
                            emptyContainerText.text = powerupStats.currentStatString;

                            availabilityScript.IsAvailable = false;

                            // Exit the loop after finding the first available slot
                            return;
                        }
                        //if it contains the string name from the already equipped list, then overwrite the stats
                        //if there is no available space AND upgrade IS equipped
                        else if (!availabilityScript.IsAvailable && equippedUpgrades.Contains(upgrade3))
                        {
                            //looks through all the upgrades we already have equipped
                            for (int j = 0; j < equippedUpgrades.Count; j++)
                            {
                                //checks if upgrade1's name matches any of the assignedUpgrade(names) we have in this equipped list
                                if (upgrade3.name == availabilityScript.assignedUpgrade)
                                {
                                    Debug.Log("Upgrade is already in equipped");
                                    //accessing the currentStat string
                                    PowerupStats powerupStats = upgrade3.GetComponent<PowerupStats>();
                                    powerupStats.UpdateLVL();
                                    //SETTING THE TEXT OF SLOT
                                    //accessing text of emptycontainer and string from upgrade
                                    TextMeshProUGUI emptyContainerText = equippedUpgradesContainer[i].GetComponentInChildren<TextMeshProUGUI>();

                                    //setting the emptycontainer text to the currentStat
                                    emptyContainerText.text = powerupStats.currentStatString;

                                    availabilityScript.IsAvailable = false;

                                    // Exit the loop after finding the first available slot
                                    return;
                                }
                            }
                        }
                        else if (!availabilityScript.IsAvailable && !equippedUpgrades.Contains(upgrade3))
                        {
                        noSlotText.text = "NO FREE SLOTS AVAILABLE FOR UNIQUE UPGRADE";
                        StartCoroutine(SetTextBack());
                        Debug.Log("No free slots available");
                        }
                    upgradeWasPicked = true;
                    }
                //}
            }
            else
            {
                Debug.LogError("Button 1 not assigned to the script.");
            }
        }
    }*/
        List<GameObject> GetRandomUpgrades(int count)
        {
            List<GameObject> randomUpgrades = new List<GameObject>();

            // Ensure there are enough powerup prefabs
            if (upgradePrefabs.Count < count)
            {
                Debug.LogWarning("Not enough powerup prefabs for the selected count.");
                return randomUpgrades;
            }

            // Shuffle the powerup prefabs to get a random order
            upgradePrefabs.Shuffle();

            // Select the first 'count' powerups from the shuffled list
            for (int i = 0; i < count; i++)
            {
                randomUpgrades.Add(upgradePrefabs[i]);
            }

            return randomUpgrades;
        }

        void DisplayUpgrades(List<GameObject> upgrades, GameObject upgradePanel)
        {
            // Ensure there are enough spawn points
            if (upgradeSpawnPoints.Count < upgrades.Count)
            {
                return;
            }

            // Loop through each powerup and instantiate it at a corresponding spawn point
            for (int i = 0; i < Mathf.Min(upgrades.Count, upgradeSpawnPoints.Count); i++)
            {
                // Use the corresponding spawn point for each powerup
                Transform spawnPoint = upgradeSpawnPoints[i];

                // Instantiate a powerup GameObject at the specified spawn point
                GameObject upgradeInstance = Instantiate(upgrades[i], spawnPoint.position, Quaternion.identity);
                upgradeInstances.Add(upgradeInstance);
                // Set the instantiated powerup as a child of the powerup panel
                upgradeInstance.transform.SetParent(upgradePanel.transform);

                // Attach any additional scripts or logic for player interaction
            }
        }
    }

    /*void UpdateUpgradeText()
    {
        //THIS FUNCTION IS FOR GETTING THE STRING INFOS AND DISPLAYING THEM FOR THE PICKABLE UPGRADES
        // Iterate through each upgrade GameObject
        int index = 0; // Track the index of the upgrade slot
        foreach (GameObject container in upgradesContainer)
        {
            // Access the TextMeshProUGUI components attached to the empty upgrade slots
            TextMeshProUGUI name = container.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI description = container.transform.Find("Description").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI stat = container.transform.Find("Stat").GetComponent<TextMeshProUGUI>();

            // Make sure that the index is within the range of selectedUpgrades
            if (index < selectedUpgrades.Count)
            {
                //WE GET THE UPGRADESTAT OF EACH UPGRADE FOUND IN THE SELECTEDUPGRADES LIST
                PowerupStats powerupStats = selectedUpgrades[index].GetComponent<PowerupStats>();

                // Make sure that powerupStats component is not null
                if (powerupStats != null)
                {
                    // Get the strings from powerupStats
                    string upgradeName = powerupStats.powerupName;
                    string upgradeDescription = powerupStats.powerupDescription;
                    string upgradeStat = "UpgradeAmount: " + powerupStats.powerupAmount.ToString();

                    // Update the TextMeshProUGUI components with the strings
                    name.text = upgradeName;
                    description.text = upgradeDescription;
                    stat.text = upgradeStat;
                }
                else
                {
                    Debug.LogWarning("powerupStats component is missing in the displayed upgrade GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Index out of range: selectedUpgrades list is smaller than emptyUpgradeSlots list.");
            }

            index++; // Increment the index for the next upgrade slot
        }
    }

    public IEnumerator SetTextBack()
    {
        yield return new WaitForSeconds(3);
        noSlotText.text = "Upgrades equipped";
    }*/
public static class ListExtensions
{
    // Fisher-Yates shuffle algorithm. A famous method for shuffling elements in a list
    //We declare a List containing generic items
    public static void Shuffle<T>(this IList<T> list)
    {
        int upgrades = list.Count; // Get the number of elements in the list

        //Iterate through the list in reverse order. Start a loop that continues as long as there are more than one element remaining to shuffle
        while (upgrades > 1)
        {
            upgrades--; // Decrease the number of remaining elements

            // Generate a random index between 0 and upgrades (inclusive)
            int randomIndex = Random.Range(0, upgrades + 1);

            // Swap the elements at positions k and n in the list
            T value = list[randomIndex];
            //replace the element at randomIndex with the element at upgrades
            list[randomIndex] = list[upgrades];
            //replace the element at upgrades with the stored value
            list[upgrades] = value;
        }
    }
}