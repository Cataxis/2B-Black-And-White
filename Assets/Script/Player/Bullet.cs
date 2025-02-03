using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int damage = 1;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.1f;
    [SerializeField] private float minVolume = 0.8f;
    [SerializeField] private float maxVolume = 1.0f;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayShotSound();
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Target target = collision.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        BubbleLife bubble = collision.GetComponent<BubbleLife>();
        if (bubble != null)
        {
            bubble.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // Nueva detección para TargetLuna
        TargetLuna targetLuna = collision.GetComponent<TargetLuna>();
        if (targetLuna != null)
        {
            targetLuna.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }
    }

    private void PlayShotSound()
    {
        if (audioSource != null && shotSound != null)
        {
            audioSource.clip = shotSound;
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = Random.Range(minVolume, maxVolume);
            audioSource.Play();
        }
    }
}
