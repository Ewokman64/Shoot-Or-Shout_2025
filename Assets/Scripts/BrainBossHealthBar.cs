using UnityEngine;
using UnityEngine.UI;

public class BrainBossHealthBar : MonoBehaviour
{
    public Slider healthSlider; // Reference to the health slider UI element

    // Adjust the maximum health as needed
    public float maxHealth = 10f;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Ensure the healthSlider is assigned in the Inspector or find it in the hierarchy
        if (healthSlider == null)
        {
            healthSlider = GetComponentInChildren<Slider>();
        }

        // Set the maximum value of the health slider
        healthSlider.maxValue = maxHealth;
        UpdateHealthBar();
    }

    // Method to update the health bar
    public void UpdateHealthBar()
    {
        Debug.Log("Healthbar Updated!");
        // Calculate the health percentage
        //float healthPercentage = currentHealth / maxHealth;

        // Update the fill amount of the health slider
        healthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
