using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyNightKnight : MonoBehaviour
{
    private Horse horse;
    private NightKnight nightKnight;
    // Start is called before the first frame update
    void Start()
    {
        horse = GameObject.Find("Horse").GetComponent<Horse>();
        nightKnight = GameObject.Find("NightKnight").GetComponent<NightKnight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (horse == null && nightKnight == null)
        {
            Destroy(gameObject);
        }
    }
}
