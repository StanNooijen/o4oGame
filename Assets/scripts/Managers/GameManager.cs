using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameOverUI;
    public GameObject ScoreUI;
    public GameObject Leaderboard;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        gameOverUI.SetActive(false);
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        gameOverUI.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            ScoreUI.SetActive(false);
            Leaderboard.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "Leaderboard")
        {
            Leaderboard.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            ScoreUI.SetActive(true);
        }
    }
}
