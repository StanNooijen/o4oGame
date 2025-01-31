using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveSpeed;
    public float moveDistance;
    private Rigidbody2D rb;
    public GameObject left;
    public GameObject right;
    

    private Vector2 pointA;
    private Vector2 pointB;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
       
        
        rb = GetComponent<Rigidbody2D>();
        pointA = left.transform.position;
        pointB = right.transform.position;
        
        int randomNumber = Random.Range(1, 3);
        if (randomNumber == 1)
        {
            target = pointA;
        }else if (randomNumber == 2)
        {
            target = pointB;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
        if ((Vector2)transform.position == pointB)
        {
            target = pointA;
        }
        else if ((Vector2)transform.position == pointA)
        {
            target = pointB;
        }
    }
}
