using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Target bossTarget;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform bossTransform;
    [SerializeField] private CanvasGroup canvasGroup; // <- Nuevo

    private bool hasFaded = false;

    void Start()
    {
        if (bossTarget != null && healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = bossTarget.MaxHealth;
            healthSlider.value = bossTarget.Health;
        }
    }

    void Update()
    {
        if (bossTarget != null && healthSlider != null)
        {
            healthSlider.value = bossTarget.Health;

            Vector3 screenPos = Camera.main.WorldToScreenPoint(bossTransform.position + offset);
            healthSlider.transform.position = screenPos;

            // Cuando la vida es 0 o menos, ocultamos el slider
            if (bossTarget.Health <= 0 && !hasFaded)
            {
                canvasGroup.alpha = 0;
                hasFaded = true;
            }
        }
    }
}
