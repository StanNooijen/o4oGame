using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPlatform : MonoBehaviour
{
    public float launchForce = 10.0f;
    private Rigidbody2D rb;
    private GameObject sumbitButton;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(Vector2.up * launchForce, ForceMode2D.Impulse);
            }
        }
        
    }
}