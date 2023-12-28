using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallHealthBar : MonoBehaviour
{
    public Slider leftSlider; // Reference to the health slider UI element
    public Slider rightSlider; // Reference to the health slider UI element

    // Adjust the maximum health as needed
    public float maxHealth = 50;
    public float leftHealth;
    public float rightHealth;

    // Start is called before the first frame update
    void Start()
    {
        leftHealth = maxHealth;
        rightHealth = maxHealth;

        // Ensure the healthSlider is assigned in the Inspector or find it in the hierarchy
        if (leftSlider == null)
        {
            leftSlider = GetComponentInChildren<Slider>();
        }

        // Set the maximum value of the health slider
        leftSlider.maxValue = maxHealth;
        rightSlider.maxValue = maxHealth;
        UpdateHealthBar();
    }

    // Method to update the health bar
    public void UpdateHealthBar()
    {
        // Calculate the health percentage

        // Update the fill amount of the health slider
        Debug.Log("WallHeatlh Update called!");
        leftHealth = maxHealth;
        rightHealth = maxHealth;
        leftSlider.value = leftHealth;
        rightSlider.value = rightHealth;
    }
}
