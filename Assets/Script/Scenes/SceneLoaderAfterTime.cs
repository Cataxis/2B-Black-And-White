using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderAfterTime : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a cargar
    public float delay = 3f; // Tiempo antes de cargar la nueva escena

    private float countdown;

    private void Start()
    {
        countdown = delay;
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("No se ha asignado un nombre de escena.");
        }
    }
}
