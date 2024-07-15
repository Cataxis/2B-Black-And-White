using UnityEngine;

public class BackgroundFader : MonoBehaviour
{
    [Header("Fade Settings")]
    [Tooltip("Velocidad del fade in y fade out")]
    public float fadeSpeed = 1.0f;
    
    [Header("Colors")]
    [Tooltip("Primer color (e.g., blanco)")]
    public Color color1 = Color.white;
    [Tooltip("Segundo color (e.g., negro)")]
    public Color color2 = Color.black;

    private Camera cam;
    private float lerpTime = 0.0f;
    private bool isFadingOut = true;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("No Camera component found on this GameObject. Please attach this script to a GameObject with a Camera component.");
            enabled = false;
            return;
        }
        cam.backgroundColor = color1;
    }

    void Update()
    {
        lerpTime += Time.deltaTime * fadeSpeed;

        if (lerpTime >= 1.0f)
        {
            lerpTime = 0.0f;
            isFadingOut = !isFadingOut;
        }

        if (isFadingOut)
        {
            cam.backgroundColor = Color.Lerp(color1, color2, lerpTime);
        }
        else
        {
            cam.backgroundColor = Color.Lerp(color2, color1, lerpTime);
        }
    }
}
