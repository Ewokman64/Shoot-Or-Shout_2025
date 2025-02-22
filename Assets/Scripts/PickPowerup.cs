using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PickPowerup : MonoBehaviour
{
    private PowerUpDisplay powerUpDisplayRef;

    public List<GameObject> powerupInventorySlots; //This list contains the empty slots we can assign our powerups to

    public List<GameObject> powerupsEquipped; //This list contains our already equipped powerups

    PowerupStats powerupStatsRef;
    // Start is called before the first frame update
    void Start()
    {
        powerUpDisplayRef = GetComponent<PowerUpDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        PowerupPicking();
    }

    public void PowerupPicking()
    {
        if (powerUpDisplayRef.powerupPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                powerUpDisplayRef.powerupButtons[0].onClick.Invoke();
                powerupStatsRef = powerUpDisplayRef.powerupsChosen[0].GetComponent<PowerupStats>();
                FindEmptySlot(powerUpDisplayRef.powerupsChosen[0]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                powerUpDisplayRef.powerupButtons[1].onClick.Invoke();
                powerupStatsRef = powerUpDisplayRef.powerupsChosen[1].GetComponent<PowerupStats>();
                FindEmptySlot(powerUpDisplayRef.powerupsChosen[1]);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                powerUpDisplayRef.powerupButtons[2].onClick.Invoke();
                powerupStatsRef = powerUpDisplayRef.powerupsChosen[2].GetComponent<PowerupStats>();
                FindEmptySlot(powerUpDisplayRef.powerupsChosen[2]);
            }
        }
    }

    public void FindEmptySlot(GameObject chosenPowerup)
    {
        if (powerupsEquipped.Count >= powerupInventorySlots.Count)
        {
            Debug.Log("Inventory is full!");
            return;
        }
        else
        {
            //First, loop through the slots and look for an available one
            for (int i = 0; i < powerupInventorySlots.Count; i++)
            {
                Availability availabilityScript = powerupInventorySlots[i].GetComponent<Availability>();

                if (availabilityScript.IsAvailable && powerupStatsRef.isEquipped == false)
                {
                    powerupsEquipped.Add(chosenPowerup);
                    availabilityScript.IsAvailable = false;
                    powerupStatsRef.isEquipped = true;
                    //Also assign image
                    Image emptySlotImage = powerupInventorySlots[i].GetComponent<Image>();
                    Image chosenPU_Image = chosenPowerup.GetComponent<Image>();

                    emptySlotImage.sprite = chosenPU_Image.sprite;

                    //Update inventory text
                    PowerupStats statReference = chosenPowerup.GetComponent<PowerupStats>();
                    Debug.Log("Reference found: " + statReference.name);

                    TextMeshProUGUI emptySlotText = powerupInventorySlots[i].GetComponentInChildren<TextMeshProUGUI>();
                    Debug.Log("EmptySlot text found: " + emptySlotText.name);

                    emptySlotText.text = statReference.currentStatDesc + " " + statReference.currentStat + " " + statReference.unit;
                    
                    return;
                }
                else if (availabilityScript.IsAvailable && powerupsEquipped.Contains(chosenPowerup))
                {
                    //Upgrade current
                    Debug.Log("Powerup already equipped. Upgrading " + chosenPowerup.name);
                    //Update inventory text
                    PowerupStats statReference = chosenPowerup.GetComponent<PowerupStats>();
                    TextMeshProUGUI emptySlotText = powerupInventorySlots[i].GetComponent<TextMeshProUGUI>();

                    emptySlotText.text = statReference.currentStatDesc + " " + statReference.currentStat;
                    return;
                }
            }
        }
    }
}
