using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_V2 : MonoBehaviour
{
    public bool isShooterChased;
    public bool isShouterChased;

    public IEnumerator ChaseShouter()
    {
        isShouterChased = true;
        isShooterChased = false;
        yield return new WaitForSeconds(10);
        isShouterChased = false;
        isShooterChased = true;
    }
}
