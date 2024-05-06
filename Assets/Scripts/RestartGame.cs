using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGame : MonoBehaviour
{
    public Button restartButton;
    private GameManager gameManager;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    public void Restart()
    {
        SceneManager.LoadScene("GamePlayScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && gameManager.isSomeoneDead == true)
        {
            restartButton.onClick.Invoke();
        }
    }

}
