using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UseShield : MonoBehaviour
{
    [Header("Shield")]
    public GameObject shield;
    public bool hasShield = false;

    public void Update()
    {
        Shield();
    }
    public void Shield()
    {
        if (Input.GetKeyDown(KeyCode.E) && hasShield)
        {
            shield.SetActive(true);

            StartCoroutine(ShieldOff());
        }
    }

    public IEnumerator ShieldOff()
    {
        yield return new WaitForSeconds(2);

        shield.SetActive(false);
    }
}
