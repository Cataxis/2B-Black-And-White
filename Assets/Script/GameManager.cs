using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int blocksLeft;

    private string[] specialScenes = { "Bella", "Kira", "Luna" };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        // Crear y configurar el objeto FBPPConfig
        FBPPConfig config = new FBPPConfig(); // Crea un objeto de configuración
        FBPP.Start(config); // Ahora pasa el objeto de configuración a FBPP.Start()

        blocksLeft = GameObject.FindGameObjectsWithTag("Block").Length;
        Debug.Log(blocksLeft);
    }

    public void BlockDestroyed()
    {
        blocksLeft--;
        Debug.Log(blocksLeft);

        if (blocksLeft <= 0)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentLevel = int.Parse(currentSceneName.Replace("Level", ""));
        UnlockNextLevel(currentLevel);

        // Cargar la escena especial si existe, o el siguiente nivel
        string nextSceneName = GetNextSceneName(currentLevel);

        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Manejar caso especial si no existe la siguiente escena, como créditos
        }
    }

    private void UnlockNextLevel(int currentLevel)
    {
        int levelReached = FBPP.GetInt("LevelReached", 1);
        if (currentLevel >= levelReached)
        {
            FBPP.SetInt("LevelReached", currentLevel + 1);
        }
    }

    private string GetNextSceneName(int currentLevel)
    {
        // Revisar si el siguiente nivel tiene una escena especial asociada
        if (currentLevel == 4) return "Bella";
        if (currentLevel == 9) return "Kira";
        if (currentLevel == 14) return "Luna";

        // Si no hay escena especial, devolver el siguiente nivel
        return "Level" + (currentLevel + 1);
    }

    public void ReloadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
