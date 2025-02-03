using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public Camera targetCamera; // C�mara que se mover�
    public float shakeDuration = 0.5f; // Duraci�n del shake en segundos
    public float shakeMagnitude = 0.1f; // Intensidad del shake

    private Vector3 originalPosition;

    void Start()
    {
        if (targetCamera == null)
        {
            Debug.LogError("No se ha asignado una c�mara al CameraShake.");
            return;
        }

        originalPosition = targetCamera.transform.position;
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        Debug.Log("Shake");
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            targetCamera.transform.position = originalPosition + new Vector3(randomOffset.x, randomOffset.y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        targetCamera.transform.position = originalPosition; // Restaurar la posici�n original
    }
}
