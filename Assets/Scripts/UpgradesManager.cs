using System.Collections;
using System.Collections.Generic;
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

    private int lastScoreThreshold = 0; // Keep track of the last threshold to avoid repeated triggering
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the current score threshold
        int currentScoreThreshold = Mathf.FloorToInt(gameManager.score / scoreThresholdIncrement) * scoreThresholdIncrement;

        // Check if the player's score meets a new threshold
        if (currentScoreThreshold > lastScoreThreshold)
        {
            // Update the last threshold to avoid repeated triggering
            lastScoreThreshold = currentScoreThreshold;

            // Randomly select three unique powerups
            List<GameObject> selectedPowerups = GetRandomPowerups(3);

            // Set the powerup panel active
            upgradePanel.SetActive(true);

            // Display the powerups to the player using the UI panel
            DisplayPowerups(selectedPowerups, upgradePanel);
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
