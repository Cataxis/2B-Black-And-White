using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseForBoss : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooter.Instance.DeadPlayer();
        }
    }
}
