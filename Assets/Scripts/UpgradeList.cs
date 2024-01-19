using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeList : MonoBehaviour
{
    private ShooterController shooterController;
    private TaunterController taunterController;
    private SpawnManager spawnManager;
    public GameObject upgradesPanel;
    public Bullet bulletPrefab; // Reference to the Bullet script attached to a prefab

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
        shooterController.bulletCDRate -= 0.15f;
        Debug.Log("Shooting Cooldown Got Shorter");
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        spawnManager.nextWaveReady = true;
    }
    public void TauntCoolDown()
    {
        GetReferences();
        taunterController.tauntCDRate -= 0.15f;
        Debug.Log("Taunt Cooldown Got Shorter");
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        spawnManager.nextWaveReady = true;
    }

    public void MovementSpeed()
    {
        GetReferences();
        Debug.Log("Movement Speed increased!");
        shooterController.speed += 1;
        taunterController.speed += 1;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        spawnManager.nextWaveReady = true;
    }

    public void PiercingAmmo()
    {
        GetReferences();
        Debug.Log("PRETEND your bullets pierce enemies");
        // Access the Bullet script without instantiating a visible GameObject
        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Set the health using the public method in Bullet script
            bulletScript.SetHealth(2f);
            upgradesPanel.SetActive(false);
            Time.timeScale = 1;
            spawnManager.nextWaveReady = true;
        }
    }


    public void PowerUpSpawnRate()
    {
        GetReferences();
        Debug.Log("PowerUp spawnrate got shorter!");
        spawnManager.powerUp_spawnRate -= 1;
        upgradesPanel.SetActive(false);
        Time.timeScale = 1;
        spawnManager.nextWaveReady = true;
    }   
}
