using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class UseShield : MonoBehaviour
{
    [Header("Shield")]
    public GameObject shield;
    public bool hasShield = false;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }
    public void Update()
    {
        Shield();
    }
    public void Shield()
    {
        if (Input.GetKeyDown(KeyCode.E) && hasShield)
        {
            shield.SetActive(true);
            audioManager.PlayShieldOn();

            StartCoroutine(ShieldOff());
        }
    }

    public IEnumerator ShieldOff()
    {
        yield return new WaitForSeconds(2);

        shield.SetActive(false);
        audioManager.PlayShieldOff();
    }
}
