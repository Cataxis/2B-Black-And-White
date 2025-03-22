using UnityEngine;
using XInputDotNetPure; // Aseg�rate de que la DLL est� en Assets/Plugins

public class SimpleVibration : MonoBehaviour
{
    [Range(0f, 1f)] public float intensity = 0.5f; // Intensidad de vibraci�n
    public float duration = 1f; // Duraci�n en segundos

    private float timer;
    private bool isVibrating = false;

    private void Start()
    {
        StartVibration();
    }

    private void Update()
    {
        if (isVibrating)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                StopVibration();
            }
        }
    }

    private void StartVibration()
    {
        GamePad.SetVibration(PlayerIndex.One, intensity, intensity);
        timer = duration;
        isVibrating = true;
    }

    private void StopVibration()
    {
        GamePad.SetVibration(PlayerIndex.One, 0, 0);
        isVibrating = false;
    }

    private void OnDestroy()
    {
        StopVibration();
    }
}
