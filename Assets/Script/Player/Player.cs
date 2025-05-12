using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float bounds = 4.5f;
    [SerializeField] private TextMeshProUGUI pauseText;
    private bool isPaused = false;
   

    void Start()
    {
        if (pauseText != null)
        {
            pauseText.gameObject.SetActive(false);
        }
        Time.timeScale = 1f;
    }

    void Update()
    {
        CheckWindowFocus();

        if (!isPaused)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            RestartScene();
        }

        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            TogglePauseGame();
        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Joystick1Button6))
        {
            ReturnToMainMenu();
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

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void TogglePauseGame()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;

        if (pauseText != null)
        {
            pauseText.gameObject.SetActive(isPaused);
        }
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Levels");
    }

    private void CheckWindowFocus()
    {
        if (!Application.isFocused)
        {
            isPaused = true;
            Time.timeScale = 0f;

            if (pauseText != null)
            {
                pauseText.gameObject.SetActive(true);
            }
        }
    }
}