using UnityEngine;

public class HorizontalMover : MonoBehaviour
{
    public enum Direction
    {
        Left,
        Right
    }

    public Direction moveDirection = Direction.Right;
    public float moveSpeed = 5f;

    void Start()
    {
        transform.position = Vector3.zero;
    }

    void Update()
    {
        Vector3 direction = moveDirection == Direction.Right ? Vector3.right : Vector3.left;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
