using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {
        // Crear y configurar el objeto FBPPConfig
        FBPPConfig config = new FBPPConfig(); // Crea un objeto de configuración
        FBPP.Start(config); // Ahora pasa el objeto de configuración a FBPP.Start()

        //PlayerPrefs.DeleteAll();
        //FBPP.DeleteAll(); // Ahora puedes usarlo después de la inicialización

        int levelReached = FBPP.GetInt("LevelReached", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i + 1 > levelReached)
            {
                levelButtons[i].interactable = false;
            }
            else
            {
                int levelIndex = i + 1;
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
            }
        }
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene("Level" + levelIndex);
    }
}
