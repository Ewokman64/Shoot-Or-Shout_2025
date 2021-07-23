﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour

{
    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlayScene");//ints and such also work.
    }
    public void Controls()
    {
        SceneManager.LoadScene("ControlsScene");//ints and such also work.
    }
    public void GameMenu()
    {
        SceneManager.LoadScene("GameMenu");//ints and such also work.
    }
}
