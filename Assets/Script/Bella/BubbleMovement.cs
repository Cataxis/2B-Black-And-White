using UnityEngine;

public class BubbleMovement : MonoBehaviour
{
    public float fallSpeed = 1f; // Cambio de nombre a fallSpeed
    public float driftSpeed = 0.5f;
    public float driftRange = 0.5f;
    public float lifetime = 12f;

    private float driftDirection;
    private float spawnTime;

    private void Start()
    {
        driftDirection = Random.Range(-driftRange, driftRange);
        spawnTime = Time.time;
    }

    private void Update()
    {
        // Movimiento hacia abajo
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;

        // Movimiento de deriva
        transform.position += Vector3.right * driftDirection * driftSpeed * Time.deltaTime;

        // Destruir la burbuja después de un tiempo
        if (Time.time - spawnTime >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
