using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    void Start()
    {
        if (audioClips.Length == 0)
        {
            Debug.LogWarning("No hay clips de audio asignados.");
            return;
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        audioSource.Play();
    }
}