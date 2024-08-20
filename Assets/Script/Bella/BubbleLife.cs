using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BubbleLife : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private float colorFlashDuration = 0.1f;
    [SerializeField] private float scaleChangeDuration = 0.1f;
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private Color damageColor = Color.red; // Color configurable, por defecto rojo

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Color originalColor;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalColor = spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            PlayDamageEffects();
        }
    }

    private void PlayDamageEffects()
    {
        if (audioSource && damageSound)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Tint to damageColor and back to original color
        spriteRenderer.DOColor(damageColor, colorFlashDuration).OnComplete(() =>
        {
            spriteRenderer.DOColor(originalColor, colorFlashDuration);
        });

        // Scale up and back to original size
        transform.DOScale(originalScale * scaleFactor, scaleChangeDuration).OnComplete(() =>
        {
            transform.DOScale(originalScale, scaleChangeDuration);
        });
    }

    private void OnDestroy()
    {
        transform.DOKill();
        spriteRenderer.DOKill();
    }
}
