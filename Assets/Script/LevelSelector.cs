using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {

        //PlayerPrefs.DeleteAll();

        int levelReached = PlayerPrefs.GetInt("LevelReached", 5); 

        // Configura los botones basados en el progreso del jugador
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false; // Bloquea el botón si el nivel no está desbloqueado
            }
            else
            {
                int levelIndex = i + 1; // Variable para capturar el índice del nivel
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex)); // Agrega el listener para cargar la escena
            }
        }
    }

    // Método para cargar la escena del nivel seleccionado
    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex); // Carga la escena correspondiente (asegúrate de que las escenas sigan este patrón de nombres)
    }

    // Método para desbloquear el siguiente nivel, llamarlo al completar un nivel
    public void UnlockNextLevel(int level)
    {
        int currentLevelReached = PlayerPrefs.GetInt("LevelReached", 1);

        if (level >= currentLevelReached)
        {
            PlayerPrefs.SetInt("LevelReached", level + 1); // Desbloquea el siguiente nivel
        }
    }
}
