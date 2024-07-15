using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleAnimator : MonoBehaviour
{
    public float minScale = 0.9f;
    public float maxScale = 1.1f;
    public float minDuration = 1.5f;
    public float maxDuration = 2.5f;
    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        AnimateTitle();
    }

    void AnimateTitle()
    {
        float randomScale = Random.Range(minScale, maxScale);
        float randomDuration = Random.Range(minDuration, maxDuration);
        transform.DOScale(originalScale * randomScale, randomDuration)
                 .SetEase(Ease.InOutSine)
                 .OnComplete(AnimateTitle);
    }
}
