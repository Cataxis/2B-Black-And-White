using UnityEngine;
using System.Collections;

public class DaggerAttack : MonoBehaviour
{
    public float waitTime = 1.0f;
    public float minSpeed = 3.0f;
    public float maxSpeed = 7.0f;
    public Color daggerColor = Color.red; // Solo un color configurable desde el inspector
    public Transform spawnPoint; // Punto al que debe dirigirse la daga

    private float speed;
    private bool isLaunched = false;
    private Vector3 launchDirection;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spawnPoint == null)
        {
            Debug.LogError("No se asignó un punto de spawn.");
            return;
        }

        StartCoroutine(WaitAndLaunch());
    }

    void Update()
    {
        if (!isLaunched)
        {
            Vector3 direction = spawnPoint.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else
        {
            transform.position += launchDirection * speed * Time.deltaTime;
        }
    }

    private IEnumerator WaitAndLaunch()
    {
        yield return new WaitForSeconds(waitTime);

        if (spawnPoint != null)
        {
            Vector3 direction = spawnPoint.position - transform.position;
            direction.y = 0; // Elimina el movimiento vertical
            launchDirection = direction.normalized;

            speed = Random.Range(minSpeed, maxSpeed);
            spriteRenderer.color = daggerColor;

            isLaunched = true;
            audioSource.Play();
        }
    }
}
