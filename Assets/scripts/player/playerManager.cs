using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerManager : MonoBehaviour
{
    private Rigidbody2D rb;

    
    private Renderer space;
    private Renderer Right;
    private Renderer Left;
    
    public GameObject Player;
    public GameObject GameOverPlatform;
    public GameObject GameOverBarier;
    public GameObject jump;
    public GameObject right;
    public GameObject left;
    private GameObject GameOverForm;
    
    void Start()
    {
        if (Player != null)
        {
            rb = Player.GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogError("Player GameObject is not assigned.");
        }
        

        if (jump != null)
        {
            space = jump.GetComponent<Renderer>();
        }
        else
        {
            Debug.LogError("Jump GameObject is not assigned.");
        }

        if (right != null)
        {
            Right = right.GetComponent<Renderer>();
        }
        else
        {
            Debug.LogError("Right GameObject is not assigned.");
        }

        if (left != null)
        {
            Left = left.GetComponent<Renderer>();
        }
        else
        {
            Debug.LogError("Left GameObject is not assigned.");
        }
    }

    void Update()
    {
        
        
        
        if (Input.GetKey(KeyCode.Space))
        {
            SetOpacity(space, 2.5f); // Change opacity to 50%
        }
        else
        {
            SetOpacity(space, 0.3f); // Change opacity to 100%
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            SetOpacity(Right, 2.5f); // Change opacity to 50%
        }
        else
        {
            SetOpacity(Right, 0.3f); // Change opacity to 100%
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            SetOpacity(Left, 2.5f); // Change opacity to 50%
        }
        else
        {
            SetOpacity(Left, 0.3f); // Change opacity to 100%
        }

        
    }

    void SetOpacity(Renderer renderer, float alpha)
    {
        if (renderer)
        {
            Material material = renderer.material;
            Color color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameOverBarier)
        {
            Destroy(GameOverBarier);
            GameOver();
        }
    }

    void GameOver()
    {
        Vector3 playerPosition = Player.transform.position;
        Vector3 gameOverPlatformPosition = new Vector3(0, playerPosition.y, playerPosition.z);
        Vector3 telePortPlayer = new Vector3(0, playerPosition.y, playerPosition.z);
        Player.transform.position = telePortPlayer;
        Instantiate(GameOverPlatform, gameOverPlatformPosition, Quaternion.identity);
    }
}