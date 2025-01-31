using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public int switchcount;
    int threshold = 70;
    
    public int Score { get; private set; }

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI HighScoreText;

    public GameObject unlocked;
    
    private int maxScore;
    
    int HighestScore = 0;
    
    

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
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        HighestScore = PlayerPrefs.GetInt("Highscore", 0);
        ScoreText.text = "Score: " + Score.ToString();
        HighScoreText.text = "Highscore: " + HighestScore.ToString();
        
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Score = 0;
            ScoreText.text = "Score: " + Score.ToString();
            HighestScore = PlayerPrefs.GetInt("Highscore", 0);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void UpdateScore(int newScore)
    {
        Score = newScore;
        ScoreText.text = "Score: " + Score.ToString();

        PlayerPrefs.SetInt("score", Score);
        PlayerPrefs.Save();
        
        HighestScore = PlayerPrefs.GetInt("Highscore", 0);

        if (Score > HighestScore)
        {
            HighestScore = Score;
            HighScoreText.text = "Highscore: " + HighestScore.ToString();
            PlayerPrefs.SetInt("Highscore", HighestScore);
            PlayerPrefs.Save();
        }
        
        int midScore = 20 + switchcount;
        maxScore = midScore;
        for (int i = 0; i < switchcount; i++)
        {
            maxScore += 50;
        }
        
        Debug.Log("threshhold: " + threshold);
        if (Score == threshold && Score < maxScore && Score >= HighestScore)
        {
            Debug.Log("Unlocked new level");
            StartCoroutine(ShowPopup());
        }
    }

    public void ResetScore()
    {
        Score = 0;
        ScoreText.text = "Score: " + Score.ToString();
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator ShowPopup()
    {
        threshold += 50;
        Debug.Log("Threshold: " + threshold);
        unlocked.SetActive(true);
        yield return new WaitForSeconds(1f);
        unlocked.SetActive(false);
        
    }
}
