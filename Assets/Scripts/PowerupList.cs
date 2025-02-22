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
    private PowerUpDisplay powerupDisplayRef;
    private GameManager gameManager;
    public Bullet bulletPrefab; // Reference to the Bullet script attached to a prefab
    public CharacterMovement shooterPrefab; // Reference to the Bullet script attached to a prefab
    public CharacterMovement shouterPrefab; // Reference to the Bullet script attached to a prefab
    private void Start()
    {

    }
    void GetReferences()
    {
        shooterController = GameObject.Find("Shooter(Clone)").GetComponent<ShooterController>();
        taunterController = GameObject.Find("Taunter(Clone)").GetComponent<TaunterController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        powerupDisplayRef = GameObject.Find("PowerupManager").GetComponent<PowerUpDisplay>();
    }
    public void ShootCoolDown()
    {
        GetReferences();

        PowerupStats powerupStats = powerupDisplayRef.powerupsInGame[0].GetComponent<PowerupStats>();
        shooterController.bulletCDRate -= powerupStats.improveAmount;
        Debug.Log("New cooldown: " + shooterController.bulletCDRate);

        ResumeGame();
    }
    public void TauntCoolDown()
    {
        GetReferences();

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
            PowerupStats powerupStats = powerupDisplayRef.powerupsInGame[3].GetComponent<PowerupStats>();

            bulletScript.piercePower += powerupStats.improveAmount;
            Debug.Log("New Bullet Pierce: " + bulletScript.piercePower);
            ResumeGame();
        }
    }
    public void PowerUpSpawnRate()
    {
        GetReferences();

        PowerupStats powerupStats = powerupDisplayRef.powerupsInGame[4].GetComponent<PowerupStats>();

        spawnManager.powerUp_spawnRate -= powerupStats.improveAmount;
        Debug.Log("New cooldown: " + spawnManager.powerUp_spawnRate);

        ResumeGame();
    }

    public void ResumeGame()
    {
        //Hide Powerup panel
        powerupPanel = GameObject.Find("PowerupPanel");
        powerupPanel.SetActive(false);

        //Resume game
        gameManager.TogglePauseMenu();

        //Continue enemy spawning
        SpawnEnemies spawnEnemiesScript = GameObject.Find("WaveManager").GetComponent<SpawnEnemies>();
        spawnEnemiesScript.StartWaves();
    }
    public void ResetStats()
    {
        GetReferences();

        PowerupStats shooterStat = powerupDisplayRef.powerupsInGame[0].GetComponent<PowerupStats>();
        shooterController.bulletCDRate = shooterStat.originalStat;
        shooterStat.isEquipped = false;

        PowerupStats shouterStat = powerupDisplayRef.powerupsInGame[1].GetComponent<PowerupStats>();
        taunterController.tauntCDRate = shouterStat.originalStat;
        shouterStat.isEquipped = false;

        PowerupStats movementStat = powerupDisplayRef.powerupsInGame[2].GetComponent<PowerupStats>();
        shooterPrefab.currentSpeed = movementStat.originalStat;
        shouterPrefab.currentSpeed = movementStat.originalStat;
        movementStat.isEquipped = false;

        PowerupStats bullet_P_Stat = powerupDisplayRef.powerupsInGame[3].GetComponent<PowerupStats>();
        bulletPrefab.piercePower = bullet_P_Stat.originalStat;
        bullet_P_Stat.isEquipped = false;

        PowerupStats dualShotStat = powerupDisplayRef.powerupsInGame[4].GetComponent<PowerupStats>();
        spawnManager.powerUp_spawnRate = dualShotStat.originalStat;
        dualShotStat.isEquipped = false;
    }
}   
