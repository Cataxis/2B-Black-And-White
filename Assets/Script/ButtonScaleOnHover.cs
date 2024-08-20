using UnityEngine;
using UnityEngine.UI;

public class ButtonScaleOnHover : MonoBehaviour
{
    // Variables para ajustar las escalas desde el Inspector
    [SerializeField] private Vector3 originalScale = Vector3.one;
    [SerializeField] private Vector3 hoveredScale = Vector3.one * 1.2f;
    [SerializeField] private float transitionSpeed = 5f;

    private RectTransform rectTransform;

    void Start()
    {
        // Obtiene el componente RectTransform del bot�n
        rectTransform = GetComponent<RectTransform>();
        // Asegura que el bot�n comience con la escala original
        rectTransform.localScale = originalScale;
    }

    public void OnPointerEnter()
    {
        // Agranda el bot�n cuando el mouse est� encima
        StopAllCoroutines();
        StartCoroutine(ScaleTo(hoveredScale));
    }

    public void OnPointerExit()
    {
        // Vuelve el bot�n a su tama�o original cuando el mouse sale
        StopAllCoroutines();
        StartCoroutine(ScaleTo(originalScale));
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
    {
        while (rectTransform.localScale != targetScale)
        {
            rectTransform.localScale = Vector3.Lerp(rectTransform.localScale, targetScale, Time.deltaTime * transitionSpeed);
            yield return null;
        }
    }
}
