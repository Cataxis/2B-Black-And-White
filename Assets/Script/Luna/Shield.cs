using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] private Target target; // Asigna el Target desde el Inspector
    [SerializeField] private GameObject shieldObject; // Asigna el GameObject del escudo desde el Inspector

    void Start()
    {
        if (shieldObject != null)
        {
            shieldObject.SetActive(false); // Asegura que el escudo esté desactivado al inicio
        }
        else
        {
            Debug.LogError("Shield object not assigned.");
        }
    }

    void Update()
    {
        CheckTargetHealth();
    }

    void CheckTargetHealth()
    {
        if (target != null && shieldObject != null)
        {
            if (target.Health <= target.MaxHealth / 3)
            {
                shieldObject.SetActive(true);
            }
        }
    }
}
