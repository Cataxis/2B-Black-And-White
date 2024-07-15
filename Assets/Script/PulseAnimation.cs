using UnityEngine;
using DG.Tweening;

public class PulseAnimation : MonoBehaviour
{
    public float scaleUp = 1.2f;
    public float scaleDown = 1.0f;
    public float duration = 0.5f;
    public int loops = -1;
    public LoopType loopType = LoopType.Yoyo;

    void Start()
    {
        transform.DOScale(scaleUp, duration).SetLoops(loops, loopType).SetEase(Ease.InOutSine);
    }
}
