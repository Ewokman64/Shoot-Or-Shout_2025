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
    float bulletpierceAmount = 1;
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
        Debug.Log("New cooldown: " + shooterController.bulletCDRate);
    }
    public void TauntCoolDown()
    {
        GetReferences();
        taunterController.tauntCDRate -= tauntCDAmount;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        upgradeSelected = true;
        Debug.Log("New cooldown: " + taunterController.tauntCDRate);
    }

    public void MovementSpeed()
    {
        GetReferences();
        shooterController.speed += movementIncAmount;
        taunterController.speed += movementIncAmount;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        upgradeSelected = true;
        Debug.Log("New speed: " + shooterController.speed);
    }

    public void PiercingAmmo()
    {
        GetReferences();
        // Access the Bullet script without instantiating a visible GameObject
        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Set the health using the public method in Bullet script
            bulletScript.bulletHealth += bulletpierceAmount;
            upgradesPanel.SetActive(false);
            Time.timeScale = 1;
            upgradeSelected = true;
            Debug.Log("Bullet Pierce: " + bulletpierceAmount);
        }
    }
    public void PowerUpSpawnRate()
    {
        GetReferences();
        spawnManager.powerUp_spawnRate -= powerUpCDAmount;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        upgradeSelected = true;
        Debug.Log("New cooldown: " + spawnManager.powerUp_spawnRate);
    }
}
