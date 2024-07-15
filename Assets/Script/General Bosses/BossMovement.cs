using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float verticalSpeed = 2f;
    public float verticalLimit = 3f; 
    public float changeDirectionTime = 2f;

    private Vector2 targetPosition;
    private float timer;

    void Start()
    {
        SetRandomTargetPosition();
        timer = changeDirectionTime;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * horizontalSpeed);

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SetRandomTargetPosition();
            timer = changeDirectionTime;
        }
    }

    void SetRandomTargetPosition()
    {
        float randomX = Random.Range(-Camera.main.orthographicSize * Camera.main.aspect, Camera.main.orthographicSize * Camera.main.aspect);
        float randomY = Random.Range(-verticalLimit, verticalLimit);
        targetPosition = new Vector2(randomX, randomY);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPosition);
    }
}
