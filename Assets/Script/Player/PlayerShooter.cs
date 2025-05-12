using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShooter : MonoBehaviour
{
    public static PlayerShooter Instance { get; private set; }

    private void Awake() => Instance = this;

    [SerializeField] private bool live;
    [SerializeField] private GameObject explosion;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float bounds = 4.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 0.5f;
    private bool isPaused = false;
    private float nextFireTime = 0f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isPaused)
        {
            Move();
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Y) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            RestartScene();
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            // TogglePauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            ReturnToMainMenu();
        }
    }

    private void Move()
    {
        if (!live) return;
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 playerPosition = transform.position;
        float normalizedMoveSpeed = moveSpeed * Time.deltaTime * 60f;

        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * normalizedMoveSpeed, -bounds, bounds);
        transform.position = playerPosition;
    }

    private void Shoot()
    {
        if (!live) return;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button0)) || Input.GetKeyDown(KeyCode.Joystick1Button1) && Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        }
    }

    private void RestartScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TogglePauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void DeadPlayer()
    {

        if (!live) return;

        live = false;

        // Change the color of the sprite to red
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }

        Instantiate(explosion, transform.position, Quaternion.identity);
        StartCoroutine(ReloadScene());
    }

    IEnumerator ReloadScene()
    {
        // Reduce the time scale to create a slow-motion effect
        Time.timeScale = 0.2f;
        yield return new WaitForSecondsRealtime(1.5f);

        // Reset the time scale back to normal
        Time.timeScale = 1f;

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Levels");
    }

}