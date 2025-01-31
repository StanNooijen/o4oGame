using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Submit : MonoBehaviour
{
    private HighscoreTable highscoreTable;
    private Restart restartScript;
    private Menu MenuScript;

    public GameObject platform;
    public GameObject form;
    public GameObject score;
    public GameObject inputfield;
    private int Score;

    void Start()
    {
        restartScript = FindObjectOfType<Restart>();
        MenuScript = FindObjectOfType<Menu>();
        
        highscoreTable = FindObjectOfType<HighscoreTable>();
        if (highscoreTable == null)
        {
            Debug.LogError("HighscoreTable not found in the scene.");
        }
        
        if (form == null)
        {
            Debug.LogError("Form GameObject is not assigned.");
        }
    }
    
    void Update()
    {
        if (!form.activeSelf)
        {
            if (Input.GetKey(KeyCode.R))
            {
                restartScript.RestartScene();
            }

            if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Backspace))
            {
                MenuScript.LoadMenu();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Score = PlayerPrefs.GetInt("score", 0);
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            score.GetComponent<Text>().text = "Score: " + Score;
            gameObject.SetActive(false);
            form.SetActive(true);
            platform.SetActive(false);
            freezeGame();
        }
    }

    private void freezeGame()
    {
        Time.timeScale = 0;
    }

    public void SubmitScore()
    {
        if (highscoreTable == null)
        {
            Debug.LogError("HighscoreTable instance is null.");
            return;
        }
    
        string playername = inputfield.GetComponent<InputField>().text.ToUpper();
        Debug.Log("Player name: " + playername);
    
        if (playername.Length >= 2 && playername.Length <= 12)
        {
            
            highscoreTable.AddHighscoreEntry(Score, playername);
            form.SetActive(false);
            score.SetActive(true);
            Time.timeScale = 1;
            Invoke("activateboost", 1);
            Debug.Log("Score submitted");
        }
        else
        {
            score.GetComponent<Text>().text = "Name must be between 2 and 12 characters";
            Invoke("changeback", 1);
        }
    }

    private void changeback()
    {
        score.GetComponent<Text>().text = "Score: " + Score;
    }

    public void activateboost()
    {
        gameObject.SetActive(true);
        platform.SetActive(true);
    }
}