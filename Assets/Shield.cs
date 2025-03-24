using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public List<string> hostile;

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
            Destroy(other.gameObject);
    }
}
