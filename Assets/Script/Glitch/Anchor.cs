using UnityEngine;
using System.Collections;

public class Anchor : MonoBehaviour
{
    public float waitTime = 1.0f;
    public float minSpeed = 3.0f;
    public float maxSpeed = 7.0f;

    private float speed;
    private bool isLaunched = false;
    private Vector3 launchDirection;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        launchDirection = Vector3.down; // Dirección de caída hacia abajo
        StartCoroutine(WaitAndLaunch());
    }

    void Update()
    {
        if (isLaunched)
        {
            transform.position += launchDirection * speed * Time.deltaTime;
        }
    }

    private IEnumerator WaitAndLaunch()
    {
        yield return new WaitForSeconds(waitTime);

        speed = Random.Range(minSpeed, maxSpeed);
        isLaunched = true;
        audioSource.Play();
    }
}
