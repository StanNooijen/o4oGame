using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handsZoom : MonoBehaviour
{
    public GameObject hand1;
    public GameObject hand2;
    public Transform targetObject;
    public float moveDistance = 0.5f;
    public float moveSpeed = 1.0f;

    private Vector3 hand1OriginalPosition;
    private Vector3 hand2OriginalPosition;
    private Vector3 hand1TargetPosition;
    private Vector3 hand2TargetPosition;
    private bool movingTowardsTarget = true;

    void Start()
    {
        hand1OriginalPosition = hand1.transform.position;
        hand2OriginalPosition = hand2.transform.position;

        Vector3 directionToTarget1 = (targetObject.position - hand1OriginalPosition).normalized;
        Vector3 directionToTarget2 = (targetObject.position - hand2OriginalPosition).normalized;
        hand1TargetPosition = hand1OriginalPosition + directionToTarget1 * moveDistance;
        hand2TargetPosition = hand2OriginalPosition + directionToTarget2 * moveDistance;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("DestroyHands" ,1f);
        }

        if (hand1 && hand2)
        {
            if (movingTowardsTarget)
            {
                hand1.transform.position = Vector3.MoveTowards(hand1.transform.position, hand1TargetPosition, moveSpeed * Time.deltaTime);
                hand2.transform.position = Vector3.MoveTowards(hand2.transform.position, hand2TargetPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(hand1.transform.position, hand1TargetPosition) < 0.01f && Vector3.Distance(hand2.transform.position, hand2TargetPosition) < 0.01f)
                {
                    movingTowardsTarget = false;
                }
            }
            else
            {
                hand1.transform.position = Vector3.MoveTowards(hand1.transform.position, hand1OriginalPosition, moveSpeed * Time.deltaTime);
                hand2.transform.position = Vector3.MoveTowards(hand2.transform.position, hand2OriginalPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(hand1.transform.position, hand1OriginalPosition) < 0.01f && Vector3.Distance(hand2.transform.position, hand2OriginalPosition) < 0.01f)
                {
                    movingTowardsTarget = true;
                }
            }
        }
    }
    
    void DestroyHands()
    {
        Destroy(hand1);
        Destroy(hand2);
    }
}