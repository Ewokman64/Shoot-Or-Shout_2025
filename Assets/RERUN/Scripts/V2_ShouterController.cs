using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouterController : MonoBehaviour
{
    public List<string> hostile;

    [Header("Shouting Settings")]
    public float tauntCoolDown;
    public float tauntCDRate = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //TaunterMovement();
        Taunt();
        TauntCoolDown();
    }
    /*void TaunterMovement()
    {
        moveUp = Vector2.up * Time.deltaTime * speed;
        moveDown = Vector2.down * Time.deltaTime * speed;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Maybe check here if a space is pressed as well?
            if (Input.GetKey(KeyCode.Space) && isDashing == false)
            {
                StartCoroutine(Dash(moveUp));
            }
            transform.Translate(Vector2.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.Space) && isDashing == false)
            {
                StartCoroutine(Dash(moveDown));
            }
            transform.Translate(Vector2.down * Time.deltaTime * speed);
        }
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector2(transform.position.x, -yRange);
        }
        if (transform.position.y > yRange)
        {
            transform.position = new Vector2(transform.position.x, yRange);
        }
    }*/
    public void Taunt()
    {
        if (Input.GetKeyDown(KeyCode.Q) && tauntCoolDown <= 0)
        {
            tauntCoolDown = tauntCDRate;
        }        
    }

    public void TauntCoolDown()
    {
        if (tauntCoolDown > 0)
        {
            tauntCoolDown -= Time.deltaTime;
        }
        else
        {
            tauntCoolDown = 0;
        }
    }
    /*public IEnumerator Dash(Vector2 direction)
    {
        isDashing = true;
        transform.Translate(direction * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }*/
}
