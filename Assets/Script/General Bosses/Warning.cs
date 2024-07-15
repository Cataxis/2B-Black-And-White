using UnityEngine;

public class Warning : MonoBehaviour
{
    public GameObject warningPrefab;
    public float delayBeforeDestroy = 1.0f;

    void Start()
    {
        Invoke("TriggerWarning", delayBeforeDestroy);
    }

    void TriggerWarning()
    {
        Instantiate(warningPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
