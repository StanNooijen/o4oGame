using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private Rigidbody2D rb;
    private bool grounded = true;
    public float maxSpeed;
    private float highestPoint = 0;
    public float acceleration;
    private float currentSpeed;

    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = speed;
        rb = GetComponent<Rigidbody2D>();
        highestPoint = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");

        // ReSharper disable once Unity.InefficientMultiplicationOrder
        Vector2 movement = new Vector2(horizontal, 0).normalized * acceleration * Time.deltaTime;
        rb.AddForce(movement * currentSpeed); // Use currentSpeed instead of speed

        // Clamp the horizontal velocity to the maximum speed
        Vector2 horizontalVelocity = new Vector2(rb.velocity.x, 0);
        horizontalVelocity = Vector2.ClampMagnitude(horizontalVelocity, maxSpeed);
        rb.velocity = new Vector2(horizontalVelocity.x, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            if (dont_destroy.instance.JumpSource)
            {
                dont_destroy.instance.JumpSource.Play();
            }
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            grounded = false;
        }

        // Update the score based on the player's height
        if (transform.position.y > highestPoint)
        {
            highestPoint = transform.position.y;
            int score = Mathf.FloorToInt(highestPoint);
            ScoreManager.instance.UpdateScore(score);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            grounded = false;
        }
    }
}