using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnMainMenu : MonoBehaviour
{
    // Tiempo en segundos antes de cargar la escena
    public float delay = 5f;
    // Nombre de la escena a cargar
    public string sceneName;

    void Start()
    {
        // Inicia la corrutina para cargar la escena después del delay
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        // Espera por el tiempo especificado
        yield return new WaitForSeconds(delay);
        // Carga la nueva escena
        SceneManager.LoadScene(sceneName);
    }
}
