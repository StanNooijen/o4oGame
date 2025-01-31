using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject Rightwall;
    public GameObject Leftwall;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.CompareTag("Rightwall"))
            {
                Player.transform.position = new Vector2(Leftwall.transform.position.x + 1, Player.transform.position.y);
            }
            else if (other.gameObject.CompareTag("Leftwall"))
            {
                Player.transform.position = new Vector2(Rightwall.transform.position.x - 1, Player.transform.position.y);
            }
        }
    }
}
