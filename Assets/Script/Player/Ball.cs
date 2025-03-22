using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 initialVelocity;
    [SerializeField] private float velocityMultiplier = 1.1f;
    [SerializeField] private AudioClip collisionSound;
    [SerializeField] private AudioClip blockCollisionSound;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private float shakeMagnitude = 0.1f;
    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float velocityDelta = 0.5f;
    [SerializeField] private float minVelocity = 0.2f;

    private bool isBallMoving;
    private Rigidbody2D ballRb;
    private bool black;

    void Start()
    {
        ballRb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Joystick1Button0)) || Input.GetKeyDown(KeyCode.Joystick1Button1) && !isBallMoving)
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
        PlaySound(collisionSound);

        if (collision.gameObject.CompareTag("Star"))
        {
            HandleStarCollision(collision);
        }
        else if (collision.gameObject.CompareTag("Block"))
        {
            HandleBlockCollision(collision);
        }

        VelocityFix();
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void HandleStarCollision(Collision2D collision)
    {
        var scale = collision.transform.localScale;
        collision.transform.localScale = scale.x >= 0.04f ? new Vector3(0.02f, 0.02f, 0f) : new Vector3(0.04f, 0.04f, 0.04f);
        ballRb.velocity *= velocityMultiplier;
    }

    private void HandleBlockCollision(Collision2D collision)
    {
        PlaySound(blockCollisionSound);
        ShakeCamera();
            if (!black)
                ChangeColors(Color.black, Color.white);
            else
                ChangeColors(Color.white, Color.black);

        Destroy(collision.gameObject);
        ballRb.velocity *= velocityMultiplier;
        GameManager.Instance.BlockDestroyed();
    }

    private void ChangeColors(Color spriteColor, Color backgroundColor)
    {
        foreach (var go in FindObjectsOfType<GameObject>())
        {
            var spriteRenderer = go.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.color = spriteColor;
            }
        }

        Camera.main.backgroundColor = backgroundColor;
        black = !black;
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

    private void VelocityFix()
    {
        Vector2 velocity = ballRb.velocity;
        velocity = FixAxisVelocity(velocity);
        ballRb.velocity = velocity;
    }

    private Vector2 FixAxisVelocity(Vector2 velocity)
    {
        if (Mathf.Abs(velocity.x) < minVelocity)
        {
            velocity.x += UnityEngine.Random.value < 0.5f ? velocityDelta : -velocityDelta;
        }

        if (Mathf.Abs(velocity.y) < minVelocity)
        {
            velocity.y += UnityEngine.Random.value < 0.5f ? velocityDelta : -velocityDelta;
        }

        return velocity;
    }
}
