using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButtons;

    void Start()
    {

        //PlayerPrefs.DeleteAll();

        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

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
