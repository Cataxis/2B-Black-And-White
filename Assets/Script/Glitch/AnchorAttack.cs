using UnityEngine;

public class AnchorAttack : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn; // Prefab a instanciar
    [SerializeField] private float spawnInterval = 2.0f; // Intervalo de tiempo en segundos

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPrefab), spawnInterval, spawnInterval);
    }

    private void SpawnPrefab()
    {
        if (prefabToSpawn != null)
        {
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, transform.rotation);
            if (spawnedPrefab != null)
            {
                // Añadir el componente AnchorAttack si no lo tiene
                if (spawnedPrefab.GetComponent<AnchorAttack>() == null)
                {
                    spawnedPrefab.AddComponent<AnchorAttack>();
                }
            }
        }
        else
        {
            Debug.LogWarning("Prefab to spawn is not set.");
        }
    }
}
