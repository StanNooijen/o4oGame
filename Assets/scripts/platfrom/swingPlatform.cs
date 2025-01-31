using UnityEngine;

public class swingPlatform : MonoBehaviour
{
    public float swingSpeed = 1f;
    public float swingAngle = 30f;

    private float startAngle;
    private Collider2D platformCollider;

    void Start()
    {
        startAngle = transform.rotation.eulerAngles.z;
        platformCollider = GetComponent<Collider2D>();

        if (platformCollider == null)
        {
            Debug.LogError("No Collider2D found on the swinging platform.");
        }
    }

    void Update()
    {
        float angle = startAngle + swingAngle * Mathf.Sin(Time.time * swingSpeed);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                playerRb.velocity = new Vector2(playerRb.velocity.x, Mathf.Max(playerRb.velocity.y, 0));
            }
        }
    }
}