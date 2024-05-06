using System.Collections.Generic;
using UnityEngine;

public class BrainBullet : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    private GameObject shooter;
    private GameObject taunter;
    private float speed = 7;

    // Start is called before the first frame update
    void Start()
    {
        shooter = GameObject.Find("Shooter(Clone)");
        taunter = GameObject.Find("Taunter(Clone)");
        targets.Add(shooter);
        targets.Add(taunter);

        /// Pick a random target
        GameObject chosenTarget = GetRandomTarget();


        //if it's shooter, go left
        if (chosenTarget.CompareTag("Shooter"))
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //if it's shouter, go right
        else if (chosenTarget.CompareTag("Taunter"))
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.Translate(Vector2.right * Time.deltaTime * speed);
        }
    }
    GameObject GetRandomTarget()
    {
        if (targets != null && targets.Count > 0)
        {
            // Choose a random index from the list
            int randomIndex = Random.Range(0, targets.Count);

            // Return the GameObject at the random index
            return targets[randomIndex];
        }
        else
        {
            // Handle the case where the list is empty
            Debug.LogWarning("No targets available.");
            return null;
        }
    }
}
