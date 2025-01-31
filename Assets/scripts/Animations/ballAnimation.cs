using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballAnimation : MonoBehaviour
{
    public float ballspeed;
    private float acceleration;
    private float changedirectionIntervall;
    private Rigidbody2D rb;
    private Vector2 direction;

    void Start()
    {
        acceleration = Random.Range(50, 200);
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChangeDirectionRoutine());
    }

    void Update()
    {
        MoveBall();
        changedirectionIntervall = Random.Range(1, 10);
    }

    private void MoveBall()
    {
        ballspeed =+ acceleration * Time.deltaTime;
        rb.AddForce(direction * ballspeed);;
    }

    private IEnumerator ChangeDirectionRoutine()
    {
        while (true)
        {
            acceleration = Random.Range(50, 200);
            yield return new WaitForSeconds(changedirectionIntervall);
            int randomDirection = Random.Range(0, 2);
            direction = randomDirection == 0 ? Vector2.left : Vector2.right;
        }
    }
}