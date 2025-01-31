using System.Collections;
using UnityEngine;

public class PlatformFalling : MonoBehaviour
{
    public int timer;
    
    private Vector3 originalPosition;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player collided with falling platform");
            StartCoroutine(Fall());
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private IEnumerator Fall()
    {
        originalPosition = transform.position;
        yield return new WaitForSeconds(timer);
        ApplyFallChanges(gameObject);

        yield return new WaitForSeconds(timer * 2);
        ResetPlatform();
    }

    private void ApplyFallChanges(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
        }

        Collider2D collider = obj.GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }
    
    private void ResetPlatform()
    {
        transform.position = originalPosition;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints |= RigidbodyConstraints2D.FreezePositionY;
            rb.constraints |= RigidbodyConstraints2D.FreezeRotation;
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = false;
        }
    }
}