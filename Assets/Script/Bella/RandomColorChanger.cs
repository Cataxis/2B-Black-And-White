using UnityEngine;
using System.Collections;

public class ColorCycler : MonoBehaviour
{
    public float transitionDuration = 0.5f; // Duración en segundos para cada transición de color
    public float changeInterval = 0.5f; // Intervalo de tiempo entre cada cambio de color

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(ChangeColorCoroutine());
    }

    private IEnumerator ChangeColorCoroutine()
    {
        while (true)
        {
            Color startColor = spriteRenderer.color;
            Color endColor = GetRandomColor();
            float elapsedTime = 0f;

            while (elapsedTime < transitionDuration)
            {
                spriteRenderer.color = Color.Lerp(startColor, endColor, elapsedTime / transitionDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            spriteRenderer.color = endColor;
            yield return new WaitForSeconds(changeInterval); // Esperar antes de cambiar a un nuevo color
        }
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
