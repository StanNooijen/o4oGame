using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class simpleFunctions : MonoBehaviour
{

    public void Close()
    {
        gameObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Game")
        {
            Time.timeScale = 1;
            Invoke("ActivateBoost", 1);
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("Leaderboard");
    }
    
}