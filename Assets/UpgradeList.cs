using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeList : MonoBehaviour
{
    private ShooterController shooterController;
    private TaunterController taunterController;
    private SpawnManager spawnManager;
    public GameObject upgradesPanel;

    private void Update()
    {
        /*if (gameObject.activeSelf)
        {
            Debug.Log("UpgradeList active! Getting relevant components");
            
            //make separate method and call them in each button?
        }*/
    }
    void GetReferences()
    {
        upgradesPanel = GameObject.Find("UpgradesPanel");
        shooterController = GameObject.Find("Shooter(Clone)").GetComponent<ShooterController>();
        taunterController = GameObject.Find("Taunter(Clone)").GetComponent<TaunterController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }
    // Start is called before the first frame update
    public void ShootCoolDown()
    {
        GetReferences();
        shooterController.bulletCDRate = 0.5f;
        Debug.Log("Shooting Cooldown Got Shorter");
        upgradesPanel.SetActive(false);
    }
    public void TauntCoolDown()
    {
        GetReferences();
        taunterController.tauntCDRate = 1;
        Debug.Log("Taunt Cooldown Got Shorter");
        upgradesPanel.SetActive(false);
    }

    public void MovementSpeed()
    {
        GetReferences();
        Debug.Log("Movement Speed increased!");
        shooterController.speed = 15;
        taunterController.speed = 15;
        upgradesPanel.SetActive(false);
    }

    public void PiercingAmmo()
    {
        GetReferences();
        Debug.Log("PRETEND your bullets pierce enemies");
        upgradesPanel.SetActive(false);
    }

    public void PowerUpSpawnRate()
    {
        GetReferences();
        Debug.Log("PowerUp spawnrate got shorter!");
        spawnManager.powerUp_spawnRate = 5;
        upgradesPanel.SetActive(false);
    }
    
}
