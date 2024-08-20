using UnityEngine;
using UnityEngine.UI;

public class BossHealthUI : MonoBehaviour
{
    [SerializeField] private Target bossTarget; 
    [SerializeField] private Slider healthSlider; 
    [SerializeField] private Vector3 offset; 
    [SerializeField] private Transform bossTransform; 

    void Start()
    {
        if (bossTarget != null && healthSlider != null)
        {
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
        }
    }
}
