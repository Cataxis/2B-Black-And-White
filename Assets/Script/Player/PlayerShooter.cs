using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float bounds = 4.5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float fireRate = 0.5f;
    private bool isPaused = false;
    private float nextFireTime = 0f;

    void Update()
    {
        if (!isPaused)
        {
            Move();
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            RestartScene();
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            TogglePauseGame();
        }
    }

    private void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        Vector2 playerPosition = transform.position;
        float normalizedMoveSpeed = moveSpeed * Time.deltaTime * 60f;

        playerPosition.x = Mathf.Clamp(playerPosition.x + moveInput * normalizedMoveSpeed, -bounds, bounds);
        transform.position = playerPosition;
    }

    private void Shoot()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button0)) || Input.GetKeyDown(KeyCode.Joystick1Button1)  && Time.time > nextFireTime)
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

}
