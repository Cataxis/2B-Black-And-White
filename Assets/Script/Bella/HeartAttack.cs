using UnityEngine;
using System.Collections;

public class HeartAttack : MonoBehaviour
{
    public float waitTime = 1.0f;
    public float minSpeed = 3.0f;
    public float maxSpeed = 7.0f;

    public Color color1 = Color.red;   // Color configurable desde el inspector
    public Color color2 = Color.blue;  // Color configurable desde el inspector
    public Color color3 = Color.green; // Color configurable desde el inspector

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

            // Seleccionar un color aleatorio entre los configurados
            Color[] colors = { color1, color2, color3 };
            Color launchColor = colors[Random.Range(0, colors.Length)];
            spriteRenderer.color = launchColor;

            isLaunched = true;
            audioSource.Play();
        }
    }
}
