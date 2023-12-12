using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class SpitterHead : MonoBehaviour
{
    public GameObject acidBall;
    public Transform acidBallSpawn;
    public int speed = 1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShootAcidBall");
    }
    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }
    public IEnumerator ShootAcidBall()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            Instantiate(acidBall, acidBallSpawn.position, transform.rotation);
            yield return new WaitForSeconds(2);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            speed *= -1;
        }
    }
}
