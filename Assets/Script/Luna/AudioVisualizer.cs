using UnityEngine;

public class AudioVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject audioSourceObject; // Objeto con el componente AudioSource
    [SerializeField] private Camera targetCamera; // Cámara para cambiar los colores
    [SerializeField] private float updateInterval = 0.1f; // Intervalo de actualización de la visualización
    [SerializeField] private float fadeSpeed = 2.0f; // Velocidad de los fades

    private AudioSource audioSource;
    private float[] samples = new float[512];
    private float timer = 0.0f;

    void Start()
    {
        if (audioSourceObject != null)
        {
            audioSource = audioSourceObject.GetComponent<AudioSource>();
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource no encontrado en el objeto asignado.");
            this.enabled = false;
        }

        if (targetCamera == null)
        {
            Debug.LogError("No se encontró la cámara asignada.");
            this.enabled = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);

            float intensity = CalculateIntensity(samples);
            UpdateCameraColor(intensity);

            timer = 0.0f;
        }
    }

    private float CalculateIntensity(float[] audioSamples)
    {
        float sum = 0.0f;

        for (int i = 0; i < audioSamples.Length; i++)
        {
            sum += audioSamples[i];
        }

        return sum / audioSamples.Length;
    }

    private void UpdateCameraColor(float intensity)
    {
        Color targetColor = (intensity > 0.1f) ? Color.white : Color.black;
        targetCamera.backgroundColor = Color.Lerp(targetCamera.backgroundColor, targetColor, fadeSpeed * Time.deltaTime);
    }
}
