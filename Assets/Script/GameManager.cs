using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool effectsActive;
    public int blocksLeft;

    private const string EffectsActiveKey = "EffectsActive";

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

        if (PlayerPrefs.HasKey(EffectsActiveKey))
        {
            int effectsActiveValue = PlayerPrefs.GetInt(EffectsActiveKey);
            effectsActive = effectsActiveValue == 1;
        }
        else
        {
            effectsActive = true; // Valor predeterminado si no se encuentra en PlayerPrefs
        }

        Debug.Log(effectsActive);
    }

    public void BlockDestroyed()
    {
        blocksLeft--;
        Debug.Log(blocksLeft);

        if (blocksLeft <= 0)
        {
            LoadNextLevel();
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}