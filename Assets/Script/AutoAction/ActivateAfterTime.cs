using UnityEngine;

public class ActivateAfterTime : MonoBehaviour
{
    public GameObject objectToActivate; // El objeto que se activará
    public float delay = 3f; // Tiempo de espera antes de activarlo

    private void Start()
    {
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // Asegurarse de que esté inactivo al inicio
            Invoke("ActivateObject", delay);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un objeto para activar.");
        }
    }

    private void ActivateObject()
    {
        objectToActivate.SetActive(true);
    }
}
