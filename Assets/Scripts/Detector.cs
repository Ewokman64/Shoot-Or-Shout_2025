using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Detector : MonoBehaviour
{

    public int health;
    public TextMeshProUGUI healthText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = ("Health: " + health.ToString());
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        //This part is for the player
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
        //This part is for the player
        if (other.gameObject.CompareTag("Zombie"))
        {
            health--;
            Destroy(other.gameObject);
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        //This part is for the player
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }
}
