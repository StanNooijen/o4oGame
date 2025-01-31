using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platfromDeleter : MonoBehaviour
{
    public GameObject GameOverPlatform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            PlatformManager.instance.RemovePlatform(collision.gameObject);
            Destroy(collision.gameObject);

            // Update the position of the GameOverPlatform
            Vector3 newPosition = new Vector3(0, GameOverPlatform.transform.position.y + 2, GameOverPlatform.transform.position.z);
            GameOverPlatform.transform.position = newPosition;
        }
    }
}