using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 initialVelocity;
    [SerializeField] private float velocityMultiplier;
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private AudioClip blockCollisionSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float shakeMagnitude;
    [SerializeField] private float shakeDuration;
    [SerializeField] private Transform cameraTransform;

    private bool isBallMoving;
    private Rigidbody2D ballRb;
    private bool black;

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && !isBallMoving)
        {
            Launch();
        }
    }

    private void Launch()
    {
        transform.parent = null;
        ballRb.velocity = initialVelocity;
        isBallMoving = true;
    }

private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collisionSound != null)
        {
            audioSource.PlayOneShot(collisionSound);
        }

        if (collision.gameObject.CompareTag("Star"))
        {
            if (collision.transform.localScale.x >= 0.04f)
            {
                collision.transform.localScale = new Vector3(0.02f, 0.02f, 0f);
            }
            else
            {
                collision.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
            }
        
            ballRb.velocity *= velocityMultiplier;
        }

        if (collision.gameObject.CompareTag("Block"))
        {
            if (blockCollisionSound != null)
            {
                audioSource.PlayOneShot(blockCollisionSound);
            }

            ShakeCamera();

            if(GameManager.Instance.effectsActive == true)
            {
                if (!black)
                {
                    ChangeColorsToBlack();
                }
                else
                {   
                    ChangeColorsToWhite();
                }
            }
            

            Destroy(collision.gameObject);
            ballRb.velocity *= velocityMultiplier;
            GameManager.Instance.BlockDestroyed();
        }

        VelocityFix();
    }

    private void ShakeCamera()
    {
        StartCoroutine(ShakeCameraCoroutine());
    }

    private IEnumerator ShakeCameraCoroutine()
    {
        Vector3 originalPosition = cameraTransform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = originalPosition.x + UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            float y = originalPosition.y + UnityEngine.Random.Range(-shakeMagnitude, shakeMagnitude);
            cameraTransform.position = new Vector3(x, y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        cameraTransform.position = originalPosition;
    }

    private void ChangeColorsToWhite()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject go in gameObjects)
        {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.white;
            }
        }

        Camera.main.backgroundColor = Color.black;
        black = false;
    }

    private void VelocityFix()
    {
        float velocityDelta = 0.5f;
        float minVelocity = 0.2f;

        if (Mathf.Abs(ballRb.velocity.x) < minVelocity)
        {
            velocityDelta = UnityEngine.Random.value < 0.5f ? velocityDelta : -velocityDelta;
            ballRb.velocity += new Vector2(velocityDelta, 0f);
        }

        if (Mathf.Abs(ballRb.velocity.y) < minVelocity)
        {
            velocityDelta = UnityEngine.Random.value < 0.5f ? velocityDelta : -velocityDelta;
            ballRb.velocity += new Vector2(0f, velocityDelta);
        }
    }

    private void ChangeColorsToBlack()
    {
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject go in gameObjects)
        {
            SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = Color.black;
            }
        }

        Camera.main.backgroundColor = Color.white;
        black = true;
    }
}
