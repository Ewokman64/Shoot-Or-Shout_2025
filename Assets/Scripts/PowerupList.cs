using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupList : MonoBehaviour
{
    private ShooterController shooterController;
    private TaunterController taunterController;
    private SpawnManager spawnManager;
    public GameObject powerupPanel;
    private PowerupManager powerupManager;
    private PowerUpDisplay powerupDisplayRef;
    //public PowerupStats powerupStats;
    private GameManager gameManager;
    public Bullet bulletPrefab; // Reference to the Bullet script attached to a prefab
    //public GameObject upgrade; // Reference to the upgrade GameObject

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
        shooterController = GameObject.Find("Shooter(Clone)").GetComponent<ShooterController>();
        taunterController = GameObject.Find("Taunter(Clone)").GetComponent<TaunterController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        powerupManager = GameObject.Find("PowerupManager").GetComponent<PowerupManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        powerupDisplayRef = GameObject.Find("PowerupManager").GetComponent<PowerUpDisplay>();

        /*foreach (GameObject powerup in powerupDisplayRef.powerupsInGame)
        {
            powerupStats = powerup.GetComponent<PowerupStats>();

            Debug.Log("Powerup Stat received: " + powerup.name);
        }*/
    }
    public void ShootCoolDown()
    {
        GetReferences();

        //shooterController.bulletCDRate -= shootCDAmount; //use improveAmount from stats
        PowerupStats powerupStats = powerupDisplayRef.powerupsInGame[0].GetComponent<PowerupStats>();

        shooterController.bulletCDRate -= powerupStats.improveAmount;
        Debug.Log("New cooldown: " + shooterController.bulletCDRate);

        ResumeGame();
    }
    public void TauntCoolDown()
    {
        GetReferences();
        //taunterController.tauntCDRate -= tauntCDAmount;

        PowerupStats powerupStats = powerupDisplayRef.powerupsInGame[1].GetComponent<PowerupStats>();

        taunterController.tauntCDRate -= powerupStats.improveAmount;
        Debug.Log("New cooldown: " + taunterController.tauntCDRate);

        ResumeGame();
    }

    public void MovementSpeed()
    {
        GetReferences();
        PowerupStats powerupStats = powerupDisplayRef.powerupsInGame[2].GetComponent<PowerupStats>();

        CharacterMovement charMovScript1 = GameObject.Find("Shooter(Clone)").GetComponent<CharacterMovement>();
        CharacterMovement charMovScript2 = GameObject.Find("Taunter(Clone)").GetComponent<CharacterMovement>();
        charMovScript1.currentSpeed += powerupStats.improveAmount;
        charMovScript2.currentSpeed += powerupStats.improveAmount;
        //shooterController.movementpeed += movementIncAmount;
        //taunterController.speed += movementIncAmount;
        Debug.Log("New speed: " + charMovScript1.currentSpeed);
        Debug.Log("New speed: " + charMovScript2.currentSpeed);
        ResumeGame();
    }

    public void PiercingAmmo()
    {
        GetReferences();
        // Access the Bullet script without instantiating a visible GameObject

        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            // Set the health using the public method in Bullet script
            //bulletScript.piercePower += bulletpierceAmount;

            PowerupStats powerupStats = powerupDisplayRef.powerupsInGame[3].GetComponent<PowerupStats>();

            bulletScript.piercePower += powerupStats.improveAmount;
            Debug.Log("New Bullet Pierce: " + bulletScript.piercePower);
            ResumeGame();
        }
    }
    public void PowerUpSpawnRate()
    {
        GetReferences();
        //spawnManager.powerUp_spawnRate -= powerUpCDAmount;

        PowerupStats powerupStats = powerupDisplayRef.powerupsInGame[4].GetComponent<PowerupStats>();

        spawnManager.powerUp_spawnRate -= powerupStats.improveAmount;
        Debug.Log("New cooldown: " + spawnManager.powerUp_spawnRate);

        ResumeGame();
    }

    public void ResumeGame()
    {
        powerupPanel = GameObject.Find("PowerupPanel");
        SpawnEnemies spawnEnemiesScript = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
        spawnEnemiesScript.StartWaves();
        //Hide Powerup panel
        powerupPanel.SetActive(false);
        //Resume game
        gameManager.TogglePauseMenu();
        //Continue enemy spawning

    }
}
