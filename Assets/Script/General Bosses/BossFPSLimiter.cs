using UnityEngine;

public class BossFPSLimiter : MonoBehaviour
{
    private int previousTargetFrameRate;

    void Awake()
    {
        // Guarda el frame rate actual y luego lo limita a 60
        previousTargetFrameRate = Application.targetFrameRate;
        Application.targetFrameRate = 60;
        Debug.Log("FPS limitados a 60 por boss en escena.");
    }

    void OnDestroy()
    {
        // Restaura el frame rate original cuando se destruya el objeto
        Application.targetFrameRate = previousTargetFrameRate;
        Debug.Log("FPS restaurados al salir del boss.");
    }
}
