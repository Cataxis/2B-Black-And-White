using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float fallSpeed = 2.0f;

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= fallSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
