using UnityEngine;

public class Finisher : MonoBehaviour
{
    public TargetLuna targetLuna; // Referencia a TargetLuna
    public GameObject objectToActivate; // Objeto a activar
    private bool isActivated = false;
    private float oneThirdHealth;

    void Start()
    {
        if (targetLuna != null)
        {
            oneThirdHealth = targetLuna.MaxHealth / 3f; // Calcula un tercio de la salud m�xima
        }
        else
        {
            Debug.LogError("TargetLuna no asignado en Finisher.");
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // Asegurar que el objeto est� desactivado al inicio
        }
        else
        {
            Debug.LogError("No se asign� un GameObject para activar.");
        }
    }

    void Update()
    {
        if (!isActivated && targetLuna != null && targetLuna.Health <= oneThirdHealth)
        {
            objectToActivate.SetActive(true);
            isActivated = true; // Evita activarlo m�ltiples veces
        }
    }
}
