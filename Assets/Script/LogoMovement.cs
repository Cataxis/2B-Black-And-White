using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LogoMovement : MonoBehaviour
{
    public Vector2 moveAmount = new Vector2(100f, 0f); // Ajusta la cantidad de movimiento en X y Y
    public float moveDuration = 1.0f; // Duración del movimiento
    public Ease moveEase = Ease.Linear; // Curva de movimiento (Ease)

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform == null)
        {
            Debug.LogError("Este script requiere un componente RectTransform adjunto al GameObject.");
            enabled = false;
        }
    }

    private void Start()
    {
        // Mueve la imagen inicialmente a su posición + moveAmount en un loop infinito y de ida y vuelta
        rectTransform.DOAnchorPos(rectTransform.anchoredPosition + moveAmount, moveDuration)
            .SetLoops(-1, LoopType.Yoyo) // Loop infinito, ida y vuelta
            .SetEase(moveEase); // Define la curva de movimiento
    }
}
