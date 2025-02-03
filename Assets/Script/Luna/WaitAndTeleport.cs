using System.Collections;
using UnityEngine;

public class WaitAndTeleport : MonoBehaviour
{
    [SerializeField] private float waitBeforeTeleport = 2f; // Tiempo antes de teletransportarse
    [SerializeField] private float targetYPosition = -4f; // Coordenada en Y a la que se moverá
    [SerializeField] private float waitAtTarget = 2f; // Tiempo en el objetivo
    [SerializeField] private float rotationSpeed = 100f; // Velocidad de rotación
    [SerializeField] private AudioSource audioSource; // Audio source para el sonido de teleport

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        StartCoroutine(TeleportRoutine());
    }

    private IEnumerator TeleportRoutine()
    {
        yield return new WaitForSeconds(waitBeforeTeleport);

        // Reproducir el sonido de teleport
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Teletransportar el objeto a la nueva posición
        transform.position = new Vector3(startPosition.x, targetYPosition, startPosition.z);

        // Rotar sobre su propio eje
        while (true)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
