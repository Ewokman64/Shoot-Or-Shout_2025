using System.Collections.Generic;
using UnityEngine;

public class BrainBullet : MonoBehaviour
{
    public List<GameObject> targets;
    private float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        /// Pick a random target
        int randomIndex = Random.Range(0, targets.Count);


        //if it's shooter, go left
        if (randomIndex == 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        //if it's shouter, go right
        else if (randomIndex == 1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<SpriteRenderer>().flipX == true)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
    }
}
