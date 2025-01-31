using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Check if the player collided with the RestartButton or MenuButton
            Debug.Log("Restarting scene");
            LoadMenu();
        }
    }
    
    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
