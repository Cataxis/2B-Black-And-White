using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseForBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(SlowTimeAndReload());
        }
    }

    private IEnumerator SlowTimeAndReload()
    {
        Time.timeScale = 0.1f;  
        yield return new WaitForSecondsRealtime(.2f);
        Time.timeScale = 1f;  
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
    }
}
