using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoveUpgrade : MonoBehaviour
{
    public GameObject upgrade; // The upgrade assigned to this slot
    public Button deleteButton; // The delete button associated with this slot

    void Start()
    {
        // Assign the onClick event for the delete button
        deleteButton.onClick.AddListener(OnDeleteButtonClick);
    }

    void OnDeleteButtonClick()
    {
        // Check if an upgrade is assigned to this slot
        if (upgrade != null)
        {
            // Remove the upgrade from this slot
            Destroy(upgrade);
            upgrade = null;
        }
        else
        {
            Debug.LogWarning("No upgrade assigned to this slot.");
        }
    }
}
