using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Target : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private float colorFlashDuration = 0.1f;
    [SerializeField] private float scaleChangeDuration = 0.1f;
    [SerializeField] private float scaleFactor = 1.2f;
    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Vector3 originalScale;
    private Color originalColor;
    private int maxHealth;

    public int Health => health;
    public int MaxHealth => maxHealth;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalScale = transform.localScale;
        originalColor = spriteRenderer.color;
        maxHealth = health;
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

        // Tint to red and back to original color
        spriteRenderer.DOColor(Color.red, colorFlashDuration).OnComplete(() =>
        {
            spriteRenderer.DOColor(originalColor, colorFlashDuration);
        });

        // Scale up and back to original size
        transform.DOScale(originalScale * scaleFactor, scaleChangeDuration).OnComplete(() =>
        {
            transform.DOScale(originalScale, scaleChangeDuration);
        });
    }
}
