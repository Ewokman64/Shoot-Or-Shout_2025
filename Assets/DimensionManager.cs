using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionManager : MonoBehaviour
{
    public GameObject DungeonDimension;
    public GameObject IceDimension;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ToIceDimension();
    }
    void ToIceDimension()
    {
        if ((gameManager.score >= 80))
        {
            IceDimension.SetActive(true);
            gameManager.ClearMap();
            DungeonDimension.SetActive(false);
        }
    }
}