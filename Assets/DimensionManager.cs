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
        if ((gameManager.easyScore >= 30) || (gameManager.normalScore >= 80) || (gameManager.hardScore >= 120))
        {
            IceDimension.SetActive(true);
            DungeonDimension.SetActive(false);
        }
    }
}