using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunaBoss : MonoBehaviour
{
    [System.Serializable]
    public class Attack
    {
        public GameObject prefab;
        public float duration;
        public Vector3 direction;
    }

    public Attack[] normalAttacks;
    public Attack[] enragedAttacks;
    public float initialInterval = 5f;
    public float enragedInitialInterval = 3f;
    public float intervalDecrement = 0.1f;
    public float minimumInterval = 1f;
    public AudioClip normalMusic;
    public AudioClip enragedMusic;
    public float musicTransitionDuration = 1f;
    public float slowMotionScale = 0.5f;
    public float slowMotionDuration = 0.5f;

    private float currentInterval;
    private int lastAttackIndex = -1;
    private bool isEnraged = false;
    [SerializeField] private Target target;
    private AudioSource audioSource;

    void Start()
    {
        currentInterval = initialInterval;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = normalMusic;
        audioSource.Play();
        StartCoroutine(ManageAttacks());
    }

    void Update()
    {
        CheckTargetHealth();
    }

    void CheckTargetHealth()
    {
        if (target != null)
        {
            if (target.Health <= target.MaxHealth / 2 && !isEnraged)
            {
                isEnraged = true;
                StartCoroutine(ChangeMusic(enragedMusic));
                currentInterval = enragedInitialInterval;
                StartCoroutine(ApplySlowMotion());
            }
        }
    }

    IEnumerator ChangeMusic(AudioClip newMusic)
    {
        float currentTime = 0;
        while (currentTime < musicTransitionDuration)
        {
            audioSource.volume = Mathf.Lerp(1f, 0f, currentTime / musicTransitionDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
        audioSource.clip = newMusic;
        audioSource.Play();
        currentTime = 0;
        while (currentTime < musicTransitionDuration)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, currentTime / musicTransitionDuration);
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator ApplySlowMotion()
    {
        Time.timeScale = slowMotionScale;
        yield return new WaitForSecondsRealtime(slowMotionDuration);
        Time.timeScale = 1f;
    }

    IEnumerator ManageAttacks()
    {
        while (true)
        {
            Attack currentAttack = GetRandomAttack();
            GameObject attackInstance = Instantiate(currentAttack.prefab, transform.position, Quaternion.identity);
            if (attackInstance.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = currentAttack.direction;
            }
            else if (attackInstance.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb2D))
            {
                rb2D.velocity = currentAttack.direction;
            }
            else
            {
                attackInstance.transform.Translate(currentAttack.direction);
            }
            yield return new WaitForSeconds(currentAttack.duration);
            Destroy(attackInstance);
            currentInterval = Mathf.Max(minimumInterval, currentInterval - intervalDecrement);
            yield return new WaitForSeconds(currentInterval);
        }
    }

    Attack GetRandomAttack()
    {
        Attack[] attackArray = isEnraged ? enragedAttacks : normalAttacks;
        List<int> possibleIndices = new List<int>();
        for (int i = 0; i < attackArray.Length; i++)
        {
            if (i != lastAttackIndex)
            {
                possibleIndices.Add(i);
            }
        }
        if (possibleIndices.Count == 0)
        {
            possibleIndices.Add(0);
        }
        int randomIndex = Random.Range(0, possibleIndices.Count);
        lastAttackIndex = possibleIndices[randomIndex];
        return attackArray[randomIndex];
    }
}
