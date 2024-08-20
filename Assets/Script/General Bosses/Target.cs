using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private float colorFlashDuration = 0.1f;
    [SerializeField] private float scaleChangeDuration = 0.1f;
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private float fadeOutDuration = 0.5f;
    [SerializeField] private GameObject fade;
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
            StartCoroutine(HandleDeath());
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

        spriteRenderer.DOColor(Color.red, colorFlashDuration).OnComplete(() =>
        {
            spriteRenderer.DOColor(originalColor, colorFlashDuration);
        });

        transform.DOScale(originalScale * scaleFactor, scaleChangeDuration).OnComplete(() =>
        {
            transform.DOScale(originalScale, scaleChangeDuration);
        });
    }

    private IEnumerator HandleDeath()
    {
        Time.timeScale = 0.4f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        if (audioSource)
        {
            audioSource.pitch = Time.timeScale;
        }

        yield return new WaitForSecondsRealtime(0.2f);

        StartCoroutine(FadeOutAllAudioSources());

        Tween fadeTween = spriteRenderer.DOFade(0, fadeOutDuration);

        if (fade != null)
        {
            fade.SetActive(true);
        }

        yield return fadeTween.WaitForCompletion();

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        DOTween.Kill(spriteRenderer);
        Destroy(gameObject);

        CompleteLevel();
    }

    private void CompleteLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Desbloquear el siguiente nivel
        int nextLevel = currentSceneIndex + 1;
        UnlockNextLevel(currentSceneIndex);

        // Cargar el siguiente nivel
        SceneManager.LoadScene(nextLevel);
    }

    private void UnlockNextLevel(int levelIndex)
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);
        if (levelIndex >= levelReached)
        {
            PlayerPrefs.SetInt("LevelReached", levelIndex);
        }
    }

    private IEnumerator FadeOutAllAudioSources()
    {
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();

        foreach (var source in audioSources)
        {
            if (source == null) continue;

            float startVolume = source.volume;
            float elapsedTime = 0f;

            while (elapsedTime < fadeOutDuration)
            {
                elapsedTime += Time.unscaledDeltaTime;
                if (source != null)
                {
                    source.volume = Mathf.Lerp(startVolume, 0, elapsedTime / fadeOutDuration);
                }
                yield return null;
            }

            if (source != null)
            {
                source.volume = 0;
                if (source.isPlaying)
                {
                    source.Stop();
                }
                Destroy(source);
            }
        }
    }
}
