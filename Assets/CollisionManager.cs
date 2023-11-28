using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    public string projectile = "Projectile";
    public string shooter = "Shooter";
    public string shouter = "Shouter";
    public string objectTypeB = "TypeB";
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered with tag: " + other.tag);

        // Check if the trigger involves objects of both specified types (tags)
        if (other.CompareTag("Projectile") && other.gameObject.CompareTag("Shooter"))
        {
            Debug.Log("Shooter collided with Projectile!");
            // Handle the logic for the collision between "Shooter" and "Projectile"
        }
        if (other.CompareTag("Projectile") && gameObject.CompareTag("Detector"))
        {
            Debug.Log("Detector collided with Projectile!");
            // Handle the logic for the collision between "Shooter" and "Projectile"
        }
    }
}
