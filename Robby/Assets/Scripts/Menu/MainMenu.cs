﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : Menu
{   

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
