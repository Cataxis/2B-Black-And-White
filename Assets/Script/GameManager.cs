using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int blocksLeft;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        blocksLeft = GameObject.FindGameObjectsWithTag("Block").Length;
        Debug.Log(blocksLeft);
    }

    public void BlockDestroyed()
    {
        blocksLeft--;
        Debug.Log(blocksLeft);

        if (blocksLeft <= 0)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Desbloquear el siguiente nivel
        int nextLevel = currentSceneIndex + 1;
        UnlockNextLevel(currentSceneIndex);

        // Cargar el siguiente nivel
        SceneManager.LoadScene(nextLevel);
    }

    private void UnlockNextLevel(int levelIndex)
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        if (levelIndex >= levelReached)
        {
            PlayerPrefs.SetInt("LevelReached", levelIndex + 1);
        }
    }

    public void ReloadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
