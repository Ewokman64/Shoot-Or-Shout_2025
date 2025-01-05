using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShouterController : MonoBehaviour
{
    public List<string> hostile;

    [Header("Shouting Settings")]
    public float tauntCoolDown;
    public float tauntCDRate = 2;

    private GameManager_V2 gameManagerScript;

    // Start is called before the first frame update
    private void Start()
    {
        gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager_V2>();
    }

    // Update is called once per frame
    void Update()
    {
        Taunt();
        TauntCoolDown();
    }
    public void Taunt()
    {
        if (Input.GetKeyDown(KeyCode.Q) && tauntCoolDown <= 0)
        {
            StartCoroutine(gameManagerScript.ChaseShouter());
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
}
