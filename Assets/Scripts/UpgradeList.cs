using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeList : MonoBehaviour
{
    private ShooterController shooterController;
    private TaunterController taunterController;
    private SpawnManager spawnManager;
    public GameObject upgradesPanel;
    private UpgradesManager upgradesManager;
    public Bullet bulletPrefab; // Reference to the Bullet script attached to a prefab
    public bool upgradeSelected = false;
    public GameObject upgrade; // Reference to the upgrade GameObject
    float shootCDAmount = 0.15f;
    float tauntCDAmount = 0.15f;
    float movementIncAmount = 1;
    float bulletHealth = 2;
    float powerUpCDAmount = 1;
    private void Update()
    {
        
    }
    void GetReferences()
    {
        upgradesPanel = GameObject.Find("UpgradesPanel");
        shooterController = GameObject.Find("Shooter(Clone)").GetComponent<ShooterController>();
        taunterController = GameObject.Find("Taunter(Clone)").GetComponent<TaunterController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        upgradesManager= GameObject.Find("UpgradesManager").GetComponent<UpgradesManager>();
    }
    public void ShootCoolDown()
    {
        GetReferences();
        shooterController.bulletCDRate -= shootCDAmount;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        upgradeSelected = true;
    }
    public void TauntCoolDown()
    {
        GetReferences();
        taunterController.tauntCDRate -= tauntCDAmount;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        upgradeSelected = true;
    }

    public void MovementSpeed()
    {
        GetReferences();
        shooterController.speed += movementIncAmount;
        taunterController.speed += movementIncAmount;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        upgradeSelected = true;
    }

    public void PiercingAmmo()
    {
        GetReferences();
        // Access the Bullet script without instantiating a visible GameObject
        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Set the health using the public method in Bullet script
            bulletScript.SetHealth(bulletHealth);
            upgradesPanel.SetActive(false);
            Time.timeScale = 1;
            upgradeSelected = true;
        }
    }
    public void PowerUpSpawnRate()
    {
        GetReferences();
        Debug.Log("PowerUp spawnrate got shorter!");
        spawnManager.powerUp_spawnRate -= powerUpCDAmount;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        upgradeSelected = true;
    }

    /*public void CallSelectUpgrade(GameObject upgrade)
    {
        upgradesManager.SelectUpgrade(upgrade);
    }*/
}
