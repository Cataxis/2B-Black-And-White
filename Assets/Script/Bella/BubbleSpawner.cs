using System.Collections;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public float spawnInterval = 1f;
    public float spawnRangeX = 5f;
    public float spawnDuration = 5f; 
    private void Start()
    {
        StartCoroutine(SpawnBubblesForDuration(spawnDuration));
    }

    private IEnumerator SpawnBubblesForDuration(float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            SpawnBubble();
            elapsedTime += spawnInterval;
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnBubble()
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX),
            transform.position.y,
            transform.position.z
        );

        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
    }
}
