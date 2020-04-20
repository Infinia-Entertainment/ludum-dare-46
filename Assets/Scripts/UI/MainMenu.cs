﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class MainMenu : MonoBehaviour
{
    public bool Hardcore;

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void setHardcore(bool isHardcore)
    {
        Hardcore = isHardcore;
    }
}