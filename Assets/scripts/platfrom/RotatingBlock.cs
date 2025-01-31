using UnityEngine;

public class RotatingBlock : MonoBehaviour
{
    public float rotationSpeed = 90f; // De rotatiesnelheid in graden per seconde

    void Update()
    {
        // Rotatie bijwerken
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
