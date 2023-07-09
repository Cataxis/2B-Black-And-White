using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Toggle toggle;

    private const string EffectsActiveKey = "EffectsActive";

    private void Start()
    {
        bool toggleValue = toggle.isOn;
        Debug.Log("Valor del Toggle en Start(): " + toggleValue);

        PlayerPrefs.SetInt(EffectsActiveKey, toggleValue ? 1 : 0);
        GameManager.Instance.effectsActive = toggleValue;

        Invoke("StartGamee", 0.2f);
    }

    private void StartGamee()
    {
        SceneManager.LoadScene("1");
    }
}