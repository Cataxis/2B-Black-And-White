using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class Achievements : MonoBehaviour
{
    public string achievementID; // ID del logro que se establece desde el inspector

    private void Start()
    {
        // Inicia sesi�n en Steam
        if (SteamClient.IsValid)
        {
            Debug.Log("Steam client is already initialized.");
            // Resetear el logro antes de desbloquearlo para pruebas
            //ResetAchievement(achievementID); // Llama al m�todo para reiniciar el logro
            UnlockAchievement(achievementID);
        }
        else
        {
            Debug.Log("Initializing Steam client...");
            SteamClient.Init(3109440, true); // Reemplaza "YourAppIdHere" con tu App ID de Steam
            if (SteamClient.IsValid)
            {
                Debug.Log("Steam client initialized successfully.");
                // Resetear el logro antes de desbloquearlo para pruebas
                //ResetAchievement(achievementID); // Llama al m�todo para reiniciar el logro
                UnlockAchievement(achievementID);
            }
            else
            {
                Debug.LogError("Failed to initialize Steam client.");
            }
        }
    }

    // M�todo para desbloquear un logro
    public void UnlockAchievement(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        achievement.Trigger();
        Debug.Log($"Achievement {id} unlocked");
    }

    // M�todo para verificar si un logro est� desbloqueado
    public void IsThisAchievementUnlock(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        Debug.Log($"Achievement {id} status: " + achievement.State);
    }

    // M�todo para reiniciar un logro
    public void ResetAchievement(string id)
    {
        var achievement = new Steamworks.Data.Achievement(id);
        achievement.Clear();
        Debug.Log($"Achievement {id} cleared");
    }

    private void OnDestroy()
    {
        // Limpieza del cliente de Steam al destruir el objeto
        if (SteamClient.IsValid)
        {
            SteamClient.Shutdown();
            Debug.Log("Steam client shutdown.");
        }
    }
}
