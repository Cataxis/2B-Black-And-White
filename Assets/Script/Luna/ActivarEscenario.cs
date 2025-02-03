using UnityEngine;

public class ActivarEscenario : MonoBehaviour
{
    [SerializeField] private TargetLuna target;
    [SerializeField] private GameObject escenario;
    private bool escenarioActivado = false;

    void Start()
    {
        if (escenario != null)
        {
            escenario.SetActive(false);
        }
    }

    void Update()
    {
        if (target != null && escenario != null && !escenarioActivado)
        {
            if (target.Health <= target.MaxHealth / 2)
            {
                escenario.SetActive(true);
                escenarioActivado = true;
            }
        }
    }
}
