using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    private GameManager gameManager;
    public List<GameObject> upgradePrefabs; // List of powerup prefabs
    public int scoreThresholdIncrement = 100; // Incremental score threshold to trigger powerup selection
    public List<Transform> upgradeSpawnPoints; // List of spawn points for displaying powerups
    public List<Transform> equippedUpgradeSpawnPoints; // List of spawn points for displaying powerups
    public GameObject upgradePanel; // Reference to the UI panel containing GameObject components
    public List<Button> upgradeButtons;
    public Button button1;
    public Button button2;
    public Button button3;
    GameObject upgrade1;
    GameObject upgrade2;
    GameObject upgrade3;
    //ASSIGN THESE THEN SET THEIR IMAGE TO THE EQUIPPED UPGRADE IMAGE
    public GameObject equippedSlot1;
    public GameObject equippedSlot2;
    public GameObject equippedSlot3;
    public bool wasUpgradeChosen = false;
    SpawnEnemies waveManager;

    //UPGRADE INVENTORY SYSTEM
    public List<GameObject> equippedUpgrades;
    public List<GameObject> equippedSlots; // List of UI slots where upgrades will be displayed

    public GameObject selectedUpgradeGameObject; // Class-level variable to store the selected upgrade GameObject
    bool isFreeSlotAvailable = true;
    // Start is called before the first frame update
    void Start()
    {
        waveManager = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonPick();
        
    }
    void CheckEquipped()
    {
        if (equippedUpgrades.Count == 3)
        {
            isFreeSlotAvailable = false;
        }
    }
    public void OfferUpgrades()
    {
        
        waveManager.StopAllCoroutines();
        wasUpgradeChosen = false;
        // Update the last threshold to avoid repeated triggering
        //lastScoreThreshold = currentScoreThreshold;

        // Randomly select three unique powerups
        upgradeButtons.Clear();
        List<GameObject> selectedUpgrades = GetRandomUpgrades(3);

        upgrade1 = selectedUpgrades[0];
        upgrade2 = selectedUpgrades[2];
        upgrade3 = selectedUpgrades[1];

        button1 = upgrade1.GetComponent<Button>();
        button2 = upgrade2.GetComponent<Button>();
        button3 = upgrade3.GetComponent<Button>();

        upgradeButtons.Add(button1);
        upgradeButtons.Add(button2);
        upgradeButtons.Add(button3);


        // Set the powerup panel active
        upgradePanel.SetActive(true);

        // Display the powerups to the player using the UI panel
        DisplayUpgrades(selectedUpgrades, upgradePanel);
    }
    public void ButtonPick()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (button1 != null)
            {
                CheckEquipped();
                if (isFreeSlotAvailable)
                {
                    // Simulate a button click
                    button1.onClick.Invoke();
                    waveManager.StartWaves();
                    wasUpgradeChosen = true;

                    // Loop through equipped slots to find the first available slot
                    for (int i = 0; i < equippedSlots.Count; i++)
                    {
                        if (!equippedUpgrades.Contains(equippedSlots[i]))
                        {
                            // Add the upgrade to the list
                            equippedUpgrades.Add(equippedSlots[i]);

                            // Get the Image components
                            Image equippedImage = equippedSlots[i].GetComponent<Image>();
                            Image upgradeImage = upgrade1.GetComponent<Image>();

                            // Set the slot's image to the upgrade's image
                            equippedImage.sprite = upgradeImage.sprite;

                            // Exit the loop after finding the first available slot
                            return;
                        }
                    }

                    Debug.LogWarning("No available slots for the upgrade.");
                    // Need function to update the text (upgrade's level, actual stats, etc)
                }
                else
                {
                    Debug.LogWarning("No free slots are available");
                }
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
                if (isFreeSlotAvailable)
                {
                    // Simulate a button click
                    button2.onClick.Invoke();
                    waveManager.StartWaves();
                    wasUpgradeChosen = true;

                    // Loop through equipped slots to find the first available slot
                    for (int i = 0; i < equippedSlots.Count; i++)
                    {
                        if (!equippedUpgrades.Contains(equippedSlots[i]))
                        {
                            // Add the upgrade to the list
                            equippedUpgrades.Add(equippedSlots[i]);

                            // Get the Image components
                            Image equippedImage = equippedSlots[i].GetComponent<Image>();
                            Image upgradeImage = upgrade1.GetComponent<Image>();

                            // Set the slot's image to the upgrade's image
                            equippedImage.sprite = upgradeImage.sprite;

                            // Exit the loop after finding the first available slot
                            return;
                        }
                    }

                    Debug.LogWarning("No available slots for the upgrade.");
                    // Need function to update the text (upgrade's level, actual stats, etc)
                }
                else
                {
                    Debug.LogWarning("No free slots are available");
                }
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
                if (isFreeSlotAvailable)
                {
                    // Simulate a button click
                    button3.onClick.Invoke();
                    waveManager.StartWaves();
                    wasUpgradeChosen = true;

                    // Loop through equipped slots to find the first available slot
                    for (int i = 0; i < equippedSlots.Count; i++)
                    {
                        if (!equippedUpgrades.Contains(equippedSlots[i]))
                        {
                            // Add the upgrade to the list
                            equippedUpgrades.Add(equippedSlots[i]);

                            // Get the Image components
                            Image equippedImage = equippedSlots[i].GetComponent<Image>();
                            Image upgradeImage = upgrade1.GetComponent<Image>();

                            // Set the slot's image to the upgrade's image
                            equippedImage.sprite = upgradeImage.sprite;

                            // Exit the loop after finding the first available slot
                            return;
                        }
                    }

                    Debug.LogWarning("No available slots for the upgrade.");
                    // Need function to update the text (upgrade's level, actual stats, etc)
                }
                else
                {
                    Debug.LogWarning("No free slots are available");
                }
            }
            else
            {
                Debug.LogError("Button 1 not assigned to the script.");
            }
        }
    }
    /*bool IsUpgradeEquipped(GameObject upgrade)
    {
        foreach (GameObject equippedUpgrade in equippedUpgrades)
        {
            if (equippedUpgrade == upgrade)
            {
                return true; // Upgrade is already equipped
            }
        }

        return false; // Upgrade is not equipped
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

            // Set the instantiated powerup as a child of the powerup panel
            upgradeInstance.transform.SetParent(upgradePanel.transform);

            // Attach any additional scripts or logic for player interaction
        }
    }
}
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
