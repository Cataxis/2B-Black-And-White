using UnityEngine;

public class Challenger : MonoBehaviour
{
    [SerializeField] private Target target; // Referencia al script Target
    [SerializeField] private float colorChangeSpeed = 1f; // Velocidad del cambio de color
    [SerializeField] private float minAlpha = 0.3f; // Valor mínimo de opacidad
    [SerializeField] private float maxAlpha = 1f; // Valor máximo de opacidad

    private SpriteRenderer spriteRenderer;
    private float hue;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (target == null)
        {
            Debug.LogError("Target no asignado en Challenger");
        }
    }

    void Update()
    {
        if (target == null || target.MaxHealth == 0) return;

        float healthPercentage = (float)target.Health / target.MaxHealth;

        // Gradúa la velocidad de cambio de color: más rápido cuando la vida es baja
        float speedMultiplier = Mathf.Lerp(1f, 10f, 1 - healthPercentage); // De lento a rápido conforme baja la vida

        // Cambia el hue de forma continua
        hue += Time.deltaTime * colorChangeSpeed * speedMultiplier;
        hue = Mathf.Repeat(hue, 1); // Mantiene el hue dentro de un rango de 0 a 1

        // Interpolación suave de la opacidad
        float alpha = Mathf.Lerp(maxAlpha, minAlpha, 1 - healthPercentage);

        // Aplica el nuevo color y opacidad al sprite
        spriteRenderer.color = Color.HSVToRGB(hue, 1, 1) * new Color(1f, 1f, 1f, alpha);
    }
}
