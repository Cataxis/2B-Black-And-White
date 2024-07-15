using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBella : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject warningPrefab;
    [SerializeField] private Transform leftSpawnPoint;
    [SerializeField] private Transform rightSpawnPoint;
    [SerializeField] private float minMoveSpeed = 3f;  // Minimum speed for random range
    [SerializeField] private float maxMoveSpeed = 7f;  // Maximum speed for random range
    [SerializeField] private float laserDuration = 5f;
    [SerializeField] private float warningDuration = 2f; // Time for warning to be visible

    private GameObject currentLaser;
    private Transform startPoint;
    private Transform endPoint;
    private float moveSpeed;

    void Start()
    {
        // Randomly choose left or right spawn position
        if (Random.value > 0.5f)
        {
            StartCoroutine(WarningAndLaser(leftSpawnPoint, rightSpawnPoint));
        }
        else
        {
            StartCoroutine(WarningAndLaser(rightSpawnPoint, leftSpawnPoint));
        }
    }

    private IEnumerator WarningAndLaser(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);  // Assign a random move speed

        // Instantiate the warning prefab
        GameObject warning = Instantiate(warningPrefab, startPoint.position, Quaternion.identity);
        yield return new WaitForSeconds(warningDuration);
        Destroy(warning);

        // Instantiate the laser prefab
        currentLaser = Instantiate(laserPrefab, startPoint.position, Quaternion.identity);
        
        // Move the laser horizontally
        StartCoroutine(MoveLaser());
    }

    private IEnumerator MoveLaser()
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = currentLaser.transform.position;
        Vector3 targetPosition = endPoint.position;
        float journeyLength = Vector3.Distance(initialPosition, targetPosition);

        while (elapsedTime < laserDuration)
        {
            float fractionOfJourney = (elapsedTime * moveSpeed) / journeyLength;
            currentLaser.transform.position = Vector3.Lerp(initialPosition, targetPosition, fractionOfJourney);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(currentLaser);
    }

    void OnDrawGizmos()
    {
        // Draw gizmos to visualize the spawn positions
        if (leftSpawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(leftSpawnPoint.position, 0.1f);
        }
        if (rightSpawnPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(rightSpawnPoint.position, 0.1f);
        }
    }
}
