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
    public UseShield useShieldRef; //Here I can turn the boolean on
    public CharacterMovement shooterPrefab; // Reference to the Bullet script attached to a prefab
    public CharacterMovement shouterPrefab; // Reference to the Bullet script attached to a prefab

    public UseShield useShieldReference;
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
    public void Shield()
    {
        GetReferences();

        UseShield shooterShield = GameObject.Find("Shooter(Clone)").GetComponent<UseShield>();
        UseShield taunterShield = GameObject.Find("Taunter(Clone)").GetComponent<UseShield>();

        shooterShield.hasShield = true;
        taunterShield.hasShield = true;

        ResumeGame();
    }
    public void TimeSlow()
    {
        GetReferences();

        PowerupStats powerupStats = powerupDisplayRef.powerupCollection[1].GetComponent<PowerupStats>();

        taunterController.isDashShoutUnlocked = true;

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
    public void DashShotPowerup()
    {
        GetReferences();

        PowerupStats powerupStats = powerupDisplayRef.powerupCollection[4].GetComponent<PowerupStats>();

        shooterController.isDashUnlocked = true;

        //powerupStats.currentStat = spawnManager.powerUp_spawnRate;

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

        UseShield shooterShield = GameObject.Find("Shooter(Clone)").GetComponent<UseShield>();
        UseShield taunterShield = GameObject.Find("Taunter(Clone)").GetComponent<UseShield>();
        shooterShield.hasShield = false;
        taunterShield.hasShield = false;

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
