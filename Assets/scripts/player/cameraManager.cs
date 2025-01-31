using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public Transform ObjectToFollow;
    public float FollowSpeed = 2f;
    public float YOffset = 2f;

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (ObjectToFollow != null)
        {
            Vector3 newPosition = new Vector3(transform.position.x, ObjectToFollow.position.y + YOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, FollowSpeed * Time.deltaTime);
        }
    }
}
