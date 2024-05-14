using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubPlayerController : MonoBehaviour
{
    float speed = 5;
    public GameObject shopButton;
    public GameObject shopKeeper;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    { 
        if (other.CompareTag("Shop")){
            shopButton.SetActive(true);
            spriteRenderer = shopKeeper.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.yellow;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shop"))
        {
            shopButton.SetActive(false);
            spriteRenderer = shopKeeper.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.green;
        }
    }
}
