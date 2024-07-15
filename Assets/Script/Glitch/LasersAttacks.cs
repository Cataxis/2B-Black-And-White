using System.Collections;
using UnityEngine;

public class LasersAttack : MonoBehaviour
{
    public Transform[] spawners;
    public GameObject prefabToSpawn;
    public int consecutiveSpawns = 5;
    public float minSpawnTime = 1.0f;
    public float maxSpawnTime = 3.0f;

    void Start()
    {
        if (spawners.Length == 0 || prefabToSpawn == null)
        {
            Debug.LogWarning("Spawners or Prefab to Spawn not assigned.");
            return;
        }

        StartCoroutine(SpawnLasers());
    }

    private IEnumerator SpawnLasers()
    {
        for (int i = 0; i < consecutiveSpawns; i++)
        {
            Transform randomSpawner = spawners[Random.Range(0, spawners.Length)];
            Instantiate(prefabToSpawn, randomSpawner.position, randomSpawner.rotation);

            float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
