using UnityEngine;

public class ActivationColliderAttack : MonoBehaviour
{
    public float activationDelay = 2.0f;

    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
            Invoke("ActivateCollider2D", activationDelay);
        }
        else
        {
            Debug.LogWarning("No Collider2D component found on this object.");
        }
    }

    void ActivateCollider2D()
    {
        if (col != null)
        {
            col.enabled = true;
        }
    }
}
