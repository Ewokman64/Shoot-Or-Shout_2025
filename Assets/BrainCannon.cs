using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainCannon : MonoBehaviour
{
    public GameObject laser;
    public Transform spawnPoint;
    public bool alreadyActive = true;
    public int numberOfLasers = 5;
    public float distanceBetweenLasers = 5;
    // Start is called before the first frame update

    private void Start()
    {
        GameObject newLaser = laser;
    }
    private void Update()
    {

    }
    public IEnumerator SpawnLasers()
    {
        yield return new WaitForSeconds(3f);
        // Instantiate a new laser
        GameObject newLaser = Instantiate(laser, spawnPoint.position, Quaternion.identity);
        //StartCoroutine(RetractLasers());
    }
}
