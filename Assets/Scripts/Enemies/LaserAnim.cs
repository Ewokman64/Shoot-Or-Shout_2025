using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAnim : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FlipLaser());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator FlipLaser()
    {
        while (true)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            yield return new WaitForSeconds(0.3f);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            yield return new WaitForSeconds(0.3f);
        }
        
    }
}
