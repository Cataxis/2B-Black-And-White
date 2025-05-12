using System.Collections.Generic;
using UnityEngine;

public class ClickerDetector : MonoBehaviour
{
    public GameObject warningUI; // Asigna el GameObject de advertencia en el Inspector

    private float lastClickTime;
    private List<float> clickIntervals = new List<float>();
    private const int MaxSamples = 10;
    private bool alreadyDetected = false;

    void Update()
    {
        if (alreadyDetected) return;

        if (Input.GetMouseButtonDown(0))
        {
            float currentTime = Time.time;
            float interval = currentTime - lastClickTime;

            if (lastClickTime != 0f)
                clickIntervals.Add(interval);

            lastClickTime = currentTime;

            if (clickIntervals.Count >= MaxSamples)
            {
                if (AreIntervalsTooSimilar(clickIntervals))
                {
                    Debug.LogWarning("🚨 ¡Posible autoclicker detectado por intervalos idénticos!");
                    HandleAutoclickerDetection();
                }

                clickIntervals.Clear();
            }
        }
    }

    bool AreIntervalsTooSimilar(List<float> intervals)
    {
        float threshold = 0.005f; // 5 milisegundos de tolerancia

        for (int i = 1; i < intervals.Count; i++)
        {
            float diff = Mathf.Abs(intervals[i] - intervals[i - 1]);
            if (diff > threshold)
                return false;
        }

        return true;
    }

    void HandleAutoclickerDetection()
    {
        alreadyDetected = true;

        // Muestra la advertencia en pantalla
        if (warningUI != null)
        {
            warningUI.SetActive(true);
        }

        // Pausa el juego
        Time.timeScale = 0f;

        // Invoca cierre del juego en 10 segundos de tiempo real
        StartCoroutine(WaitAndQuitGame(10f));
    }

    System.Collections.IEnumerator WaitAndQuitGame(float waitSeconds)
    {
        yield return new WaitForSecondsRealtime(waitSeconds);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
