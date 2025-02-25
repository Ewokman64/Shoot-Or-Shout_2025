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

        PowerupStats powerupStats = powerupDisplayRef.powerupCollection[0].GetComponent<PowerupStats>();
        shooterController.bulletCDRate -= powerupStats.improveAmount;
        Debug.Log("New cooldown: " + shooterController.bulletCDRate);

        powerupStats.currentStat = shooterController.bulletCDRate;

        ResumeGame();
    }
    public void TauntCoolDown()
    {
        GetReferences();

        PowerupStats powerupStats = powerupDisplayRef.powerupCollection[1].GetComponent<PowerupStats>();
        taunterController.tauntCDRate -= powerupStats.improveAmount;
        Debug.Log("New cooldown: " + taunterController.tauntCDRate);

        powerupStats.currentStat = taunterController.tauntCDRate;

        ResumeGame();
    }

    public void MovementSpeed()
    {
        GetReferences();
        PowerupStats powerupStats = powerupDisplayRef.powerupCollection[2].GetComponent<PowerupStats>();
        CharacterMovement charMovScript1 = GameObject.Find("Shooter(Clone)").GetComponent<CharacterMovement>();
        CharacterMovement charMovScript2 = GameObject.Find("Taunter(Clone)").GetComponent<CharacterMovement>();
        charMovScript1.currentSpeed += powerupStats.improveAmount;
        charMovScript2.currentSpeed += powerupStats.improveAmount;

        Debug.Log("New speed: " + charMovScript1.currentSpeed);
        Debug.Log("New speed: " + charMovScript2.currentSpeed);

        powerupStats.currentStat = charMovScript1.currentSpeed;

        ResumeGame();
    }

    public void PiercingAmmo()
    {
        GetReferences();
        // Access the Bullet script without instantiating a visible GameObject

        Bullet bulletScript = bulletPrefab.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            PowerupStats powerupStats = powerupDisplayRef.powerupCollection[3].GetComponent<PowerupStats>();

            bulletScript.piercePower += powerupStats.improveAmount;
            Debug.Log("New Bullet Pierce: " + bulletScript.piercePower);

            powerupStats.currentStat = bulletScript.piercePower;

            ResumeGame();
        }
    }
    public void PowerUpSpawnRate()
    {
        GetReferences();

        PowerupStats powerupStats = powerupDisplayRef.powerupCollection[4].GetComponent<PowerupStats>();

        spawnManager.powerUp_spawnRate -= powerupStats.improveAmount;
        Debug.Log("New cooldown: " + spawnManager.powerUp_spawnRate);

        powerupStats.currentStat = spawnManager.powerUp_spawnRate;

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

        PowerupStats shooterStat = powerupDisplayRef.powerupCollection[0].GetComponent<PowerupStats>();
        shooterController.bulletCDRate = shooterStat.originalStat;
        shooterStat.isEquipped = false;

        shooterStat.currentStat = shooterStat.originalStat;

        PowerupStats shouterStat = powerupDisplayRef.powerupCollection[1].GetComponent<PowerupStats>();
        taunterController.tauntCDRate = shouterStat.originalStat;
        shouterStat.isEquipped = false;

        shouterStat.currentStat = shouterStat.originalStat;

        PowerupStats movementStat = powerupDisplayRef.powerupCollection[2].GetComponent<PowerupStats>();
        shooterPrefab.currentSpeed = movementStat.originalStat;
        shouterPrefab.currentSpeed = movementStat.originalStat;
        movementStat.isEquipped = false;

        movementStat.currentStat = movementStat.originalStat;

        PowerupStats bullet_P_Stat = powerupDisplayRef.powerupCollection[3].GetComponent<PowerupStats>();
        bulletPrefab.piercePower = bullet_P_Stat.originalStat;
        bullet_P_Stat.isEquipped = false;

        bullet_P_Stat.currentStat = bullet_P_Stat.originalStat;

        PowerupStats dualShotStat = powerupDisplayRef.powerupCollection[4].GetComponent<PowerupStats>();
        spawnManager.powerUp_spawnRate = dualShotStat.originalStat;
        dualShotStat.isEquipped = false;

        dualShotStat.currentStat= dualShotStat.originalStat;
    }
}   
