using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PickPowerup : MonoBehaviour
{
    private PowerUpDisplay powerUpDisplayRef;

    GameObject powerupRef;
    // Start is called before the first frame update
    void Start()
    {
        powerUpDisplayRef = GetComponent<PowerUpDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        //CHECK INPUT 1-3, RUN ONCLICK ACCORDINGLY

        PowerupPicking();
    }

    public void PowerupPicking()
    {
        if (powerUpDisplayRef.powerupPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                powerUpDisplayRef.powerupButtons[0].onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                powerUpDisplayRef.powerupButtons[1].onClick.Invoke();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                powerUpDisplayRef.powerupButtons[2].onClick.Invoke();
            }
        }
    }

    //NEXT UP: Put powerup in inventory, check free spaces etc.
}
