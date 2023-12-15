using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainCannon : MonoBehaviour
{
    public GameObject laser;
    private GameObject[] lasers;
    public Transform spawnPoint;
    public int numberOfLasers = 8;
    public float distanceBetweenLasers = 5;
    // Start is called before the first frame update

    private void Start()
    {
        StartCoroutine(SpawnLasers());
    }
    public IEnumerator SpawnLasers()
    {
        lasers = new GameObject[numberOfLasers];
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < lasers.Length; i++)
        {
            // Calculate the position for the laser
            Vector3 spawnPosition = spawnPoint.position + new Vector3(i * distanceBetweenLasers, 0f, 0f);

            // Instantiate a new laser
            GameObject newlaser = Instantiate(laser, spawnPosition, Quaternion.identity);
            lasers[i] = newlaser;

            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.3f);
        StartCoroutine(RetractLasers());
    }
    public IEnumerator RetractLasers()
    {
        float delayBetweenRetracts = 0.1f;

        // Retract the left tentacles
        for (int i = lasers.Length - 1; i >= 0; i--)
        {
            Destroy(lasers[i]);
            yield return new WaitForSeconds(delayBetweenRetracts);
        }
    }
}
