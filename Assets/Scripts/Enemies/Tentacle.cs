using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public GameObject tentaclePrefab;
    private GameObject[] rightTentacles;
    private GameObject[] leftTentacles;
    public Transform spawnPoint;
    public int numberOfTentacles = 8;
    public float distanceBetweenTentacles = 2f;

    public IEnumerator SpawnTentacles()
    {
        rightTentacles = new GameObject[numberOfTentacles];
        leftTentacles = new GameObject[numberOfTentacles];

        for (int i = 0; i < numberOfTentacles; i++)
        {
            // Calculate the position for the new tentacle on the right
            Vector3 spawnPosition = spawnPoint.position + new Vector3(i * distanceBetweenTentacles, 0f, 0f);

            // Calculate the position for the new tentacle on the left
            Vector3 spawnPosition2 = spawnPoint.position - new Vector3(i * distanceBetweenTentacles, 0f, 0f);

            // Instantiate a new tentacle on the right
            GameObject newTentacle = Instantiate(tentaclePrefab, spawnPosition, Quaternion.identity);
            rightTentacles[i] = newTentacle;

            // Instantiate a new tentacle on the left
            GameObject newTentacle2 = Instantiate(tentaclePrefab, spawnPosition2, Quaternion.identity);
            leftTentacles[i] = newTentacle2;

            // Set the boss as the parent of the tentacles
            newTentacle.transform.parent = transform;
            newTentacle2.transform.parent = transform;

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.3f);
        StartCoroutine(RetractLeftTentacles());
        StartCoroutine(RetractRightTentacles());
    }

    public IEnumerator RetractLeftTentacles()
    {
        float delayBetweenRetracts = 0.1f;

        // Retract the left tentacles
        for (int i = leftTentacles.Length - 1; i >= 0; i--)
        {
            Destroy(leftTentacles[i]);
            yield return new WaitForSeconds(delayBetweenRetracts);
        }
    }

    public IEnumerator RetractRightTentacles()
    {
        float delayBetweenRetracts = 0.1f;

        // Retract the right tentacles
        for (int i = rightTentacles.Length - 1; i >= 0; i--)
        {
            Destroy(rightTentacles[i]);
            yield return new WaitForSeconds(delayBetweenRetracts);
        }
    }
}
