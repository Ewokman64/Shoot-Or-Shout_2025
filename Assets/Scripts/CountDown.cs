using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    private int countDownTime = 4;
    public TextMeshProUGUI countDownDisplay;
    public GameObject powerUpCanvas;
    public TextMeshProUGUI shootCDText;
    public TextMeshProUGUI shoutCDText;
    TaunterController taunterController;
    ShooterController shooterController;
    GameManager gameManager;
    public void GetPlayerCD()
    {   
        shooterController = GameObject.Find("Shooter(Clone)").GetComponent<ShooterController>();
        taunterController = GameObject.Find("Taunter(Clone)").GetComponent<TaunterController>(); 
    }
    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }
    private void Update()
    {
        //IF PLAYER SHOOTS DARKEN THE SHOOTUI FOR 1 SECOND
        if (gameManager.shooter != null)
        {
            shootCDText.text = shooterController.bulletCoolDown.ToString();
        }
        if (gameManager.taunter != null)
        {
            shoutCDText.text = taunterController.tauntCoolDown.ToString();
        }  
    }
    public IEnumerator CountDownPowerUp()
    {
        while (countDownTime > 0)
        {
            countDownDisplay.text = countDownTime.ToString();

            yield return new WaitForSeconds(1f);

            countDownTime--;
        }

        yield return new WaitForSeconds(1f);

        powerUpCanvas.gameObject.SetActive(false);
        countDownTime = 4;
    }
}
