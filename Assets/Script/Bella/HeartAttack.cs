using UnityEngine;
using System.Collections;

public class HeartAttack : MonoBehaviour
{
    public float waitTime = 1.0f;
    public float minSpeed = 3.0f;
    public float maxSpeed = 7.0f;
    public Color launchColor = new Color(1f, 0.2f, 0.6f); // Color configurable desde el inspector

    private float speed;
    private bool isLaunched = false;
    private Vector3 launchDirection;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Transform player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Buscar el objeto con el tag "Player"
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró ningún objeto con el tag 'Player'.");
        }

        StartCoroutine(WaitAndLaunch());
    }

    void Update()
    {
        if (player == null)
        {
            // Si no se encuentra el jugador, no hacer nada
            return;
        }

        if (!isLaunched)
        {
            Vector3 direction = player.position - transform.position;
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

        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            launchDirection = direction.normalized;

            speed = Random.Range(minSpeed, maxSpeed);

            spriteRenderer.color = launchColor; // Usar el color configurado desde el inspector

            isLaunched = true;
            audioSource.Play();
        }
    }
}
