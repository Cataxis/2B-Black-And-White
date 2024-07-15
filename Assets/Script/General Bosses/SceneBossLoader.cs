using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBossLoader : MonoBehaviour
{
    [SerializeField]
    float time;
    void Start()
    {
        StartCoroutine(LoadNextSceneAfterDelay(time));
    }

    IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
