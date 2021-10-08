using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountDown : MonoBehaviour
{
    private int countDownTime = 5;
    public TextMeshProUGUI countDownDisplay;
    public GameObject powerUpCanvas;

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
        countDownTime = 5;
    }
}
