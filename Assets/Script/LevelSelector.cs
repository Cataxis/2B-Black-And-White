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
                levelButtons[i].interactable = false; // Bloquea el bot�n si el nivel no est� desbloqueado
            }
            else
            {
                int levelIndex = i + 1; // Variable para capturar el �ndice del nivel
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex)); // Agrega el listener para cargar la escena
            }
        }
    }

    // M�todo para cargar la escena del nivel seleccionado
    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex); // Carga la escena correspondiente (aseg�rate de que las escenas sigan este patr�n de nombres)
    }

    // M�todo para desbloquear el siguiente nivel, llamarlo al completar un nivel
    public void UnlockNextLevel(int level)
    {
        int currentLevelReached = PlayerPrefs.GetInt("LevelReached", 1);

        if (level >= currentLevelReached)
        {
            PlayerPrefs.SetInt("LevelReached", level + 1); // Desbloquea el siguiente nivel
        }
    }
}
