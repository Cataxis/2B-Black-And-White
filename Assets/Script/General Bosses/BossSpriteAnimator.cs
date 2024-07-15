using UnityEngine;
using DG.Tweening;

public class BossSpriteAnimator : MonoBehaviour
{
    public float minY = -10f;
    public float maxY = 10f;
    public float duration = 3f;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.localPosition;
        AnimateBossSprite();
    }

    void AnimateBossSprite()
    {
        float randomY = Random.Range(minY, maxY);
        transform.DOLocalMoveY(originalPosition.y + randomY, duration)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
