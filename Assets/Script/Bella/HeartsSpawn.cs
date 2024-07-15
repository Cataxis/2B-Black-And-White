using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeartsSpawn : MonoBehaviour
{
    [Header("Spawn Points")]
    public Transform[] spawnPoints; // Array de puntos de spawn

    [Header("Prefab a Instanciar")]
    public GameObject prefab;

    [Header("Cantidad de Prefabs a Instanciar")]
    public int prefabsToSpawn = 3; // Cantidad de prefabs a instanciar (definido desde el Inspector)

    [Header("Tiempo de Instancia")]
    public float timeBetweenSpawns1 = 1.0f; // Tiempo entre la primera y segunda instancia
    public float timeBetweenSpawns2 = 1.0f; // Tiempo entre la segunda y tercera instancia

    void Start()
    {
        if (prefabsToSpawn > spawnPoints.Length)
        {
            Debug.LogError("La cantidad de prefabs a instanciar no puede ser mayor que la cantidad de puntos de spawn.");
            return;
        }

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            availableIndices.Add(i);
        }

        for (int i = 0; i < prefabsToSpawn; i++)
        {
            if (availableIndices.Count == 0)
            {
                Debug.LogWarning("No hay suficientes puntos de spawn disponibles.");
                yield break;
            }

            int randomIndex = Random.Range(0, availableIndices.Count);
            int spawnIndex = availableIndices[randomIndex];
            availableIndices.RemoveAt(randomIndex);

            Instantiate(prefab, spawnPoints[spawnIndex].position, Quaternion.identity);

            if (i == 0)
            {
                yield return new WaitForSeconds(timeBetweenSpawns1);
            }
            else if (i == 1)
            {
                yield return new WaitForSeconds(timeBetweenSpawns2);
            }
        }
    }
}
