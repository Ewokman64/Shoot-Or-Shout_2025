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


    public SpriteRenderer shieldUI_Renderer; //<- This is for the Shout ICON on the bottom.
    public float darkenAmount = 0.5f; // Value between 0 and 1
    private Color originalColor;


    private void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        shieldUI_Renderer = GameObject.Find("ShieldSkillSprite").GetComponent<SpriteRenderer>();
        // Get the current color of the sprite
        originalColor = shieldUI_Renderer.color;
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
            // Darken the color by multiplying it with a darker shade
            Color darkenedColor = originalColor * darkenAmount;

            // Set the darkened color to the sprite
            shieldUI_Renderer.color = darkenedColor;

            StartCoroutine(ShieldOff());
        }
    }

    public IEnumerator ShieldOff()
    {
        yield return new WaitForSeconds(2);

        shield.SetActive(false);
        audioManager.PlayShieldOff();

        shieldUI_Renderer.color = originalColor;
    }
}
