using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour
{
    public GameObject tentaclePrefab;
    private GameObject[] tentacles;
    public Transform spawnPoint;

    public int numberOfTentacles = 8;
    public float distanceBetweenTentacles = 2f;

    public IEnumerator SpawnTentacles()
    {
        for (int i = 0; i < numberOfTentacles; i++)
        {
            // Calculate the position for the new tentacle on the right
            Vector3 spawnPosition = spawnPoint.position + new Vector3(i * distanceBetweenTentacles, 0f, 0f);

            // Calculate the position for the new tentacle on the left
            Vector3 spawnPosition2 = spawnPoint.position - new Vector3(i * distanceBetweenTentacles, 0f, 0f);

            // Instantiate a new tentacle on the right
            GameObject newTentacle = Instantiate(tentaclePrefab, spawnPosition, Quaternion.identity);

            // Instantiate a new tentacle on the left
            GameObject newTentacle2 = Instantiate(tentaclePrefab, spawnPosition2, Quaternion.identity);

            // Set the boss as the parent of the tentacles
            newTentacle.transform.parent = transform;
            newTentacle2.transform.parent = transform;

            yield return new WaitForSeconds(0.1f);
        }
        RetractTentacles();
    }
    public IEnumerator RetractTentacles()
    {
        yield return new WaitForSeconds(1);
        // Assuming tentacles are direct children of the boss
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
