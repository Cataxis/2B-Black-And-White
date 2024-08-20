using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRain : MonoBehaviour
{
    [SerializeField] private GameObject targetObject; // El objeto que se activar�
    [SerializeField] private float delay = 26.0f; // El tiempo en segundos para activar el objeto

    void Start()
    {
        if (targetObject != null)
        {
            // Iniciar la corrutina para activar el objeto despu�s del retraso
            StartCoroutine(ActivateObjectAfterDelay());
        }
        else
        {
            Debug.LogError("No se ha asignado ning�n objeto a activar.");
        }
    }

    private IEnumerator ActivateObjectAfterDelay()
    {
        // Esperar la cantidad de tiempo especificada
        yield return new WaitForSeconds(delay);

        // Activar el objeto
        targetObject.SetActive(true);
    }
}