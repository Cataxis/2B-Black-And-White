using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenes : MonoBehaviour
{
    [SerializeField] private string sceneName = ""; 
    [SerializeField] private string creditsSceneName = "Credits";

    public void ExitGame()
    {
        Application.Quit(); 
        
    }

    public void PlayNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void EsceneWithName()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneName);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsSceneName);
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
