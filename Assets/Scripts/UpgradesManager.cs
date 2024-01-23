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
    public GameObject upgradePanel; // Reference to the UI panel containing GameObject components
    public List<Button> upgradeButtons;
    public Button button1;
    public Button button2;
    public Button button3;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ButtonPick();
    }

    public void OfferUpgrades()
    {
        // Update the last threshold to avoid repeated triggering
        //lastScoreThreshold = currentScoreThreshold;

        // Randomly select three unique powerups
        upgradeButtons.Clear();
        List<GameObject> selectedPowerups = GetRandomPowerups(3);
        GameObject upgrade1 = selectedPowerups[0];
        button1 = upgrade1.GetComponent<Button>();
        GameObject upgrade2 = selectedPowerups[1];
        button2 = upgrade2.GetComponent<Button>();
        GameObject upgrade3 = selectedPowerups[2];
        button3 = upgrade3.GetComponent<Button>();
        upgradeButtons.Add(button1);
        upgradeButtons.Add(button2);
        upgradeButtons.Add(button3);


        // Set the powerup panel active
        upgradePanel.SetActive(true);

        // Display the powerups to the player using the UI panel
        DisplayPowerups(selectedPowerups, upgradePanel);
    }
    public void ButtonPick()
    {
        // Check if a specific key is pressed (e.g., the space key)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Check if the button is assigned
            if (button1 != null)
            {
                // Simulate a button click
                button1.onClick.Invoke();
            }
            else
            {
                Debug.LogError("Button not assigned to the script.");
            }
        }
        // Check if a specific key is pressed (e.g., the space key)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Check if the button is assigned
            if (button2 != null)
            {
                // Simulate a button click
                button2.onClick.Invoke();
            }
            else
            {
                Debug.LogError("Button not assigned to the script.");
            }
        }
        // Check if a specific key is pressed (e.g., the space key)
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // Check if the button is assigned
            if (button3 != null)
            {
                // Simulate a button click
                button3.onClick.Invoke();
            }
            else
            {
                Debug.LogError("Button not assigned to the script.");
            }
        }
    }
    List<GameObject> GetRandomPowerups(int count)
    {
        List<GameObject> randomPowerups = new List<GameObject>();

        // Ensure there are enough powerup prefabs
        if (upgradePrefabs.Count < count)
        {
            Debug.LogWarning("Not enough powerup prefabs for the selected count.");
            return randomPowerups;
        }

        // Shuffle the powerup prefabs to get a random order
        upgradePrefabs.Shuffle();

        // Select the first 'count' powerups from the shuffled list
        for (int i = 0; i < count; i++)
        {
            randomPowerups.Add(upgradePrefabs[i]);
        }

        return randomPowerups;
    }

    void DisplayPowerups(List<GameObject> powerups, GameObject upgradePanel)
    {
        // Ensure there are enough spawn points
        if (upgradeSpawnPoints.Count < powerups.Count)
        {
            Debug.LogWarning("Not enough spawn points for the selected powerups.");
            return;
        }

        // Loop through each powerup and instantiate it at a corresponding spawn point
        for (int i = 0; i < Mathf.Min(powerups.Count, upgradeSpawnPoints.Count); i++)
        {
            // Use the corresponding spawn point for each powerup
            Transform spawnPoint = upgradeSpawnPoints[i];

            // Instantiate a powerup GameObject at the specified spawn point
            GameObject powerupInstance = Instantiate(powerups[i], spawnPoint.position, Quaternion.identity);

            // Set the instantiated powerup as a child of the powerup panel
            powerupInstance.transform.SetParent(upgradePanel.transform);

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
