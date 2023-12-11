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
        Destroy(other.gameObject);
        //This part is for the player
        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;          
        }
        else if (other.gameObject.CompareTag("BigEnemy"))
        {
            health--;
            health--;
            health--;
        }
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        Destroy(other.gameObject);
    }
}
