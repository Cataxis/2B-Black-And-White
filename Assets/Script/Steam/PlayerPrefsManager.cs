using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 90;

        var config = new FBPPConfig()
        {
            // Asegúrate de usar propiedades válidas para FBPPConfig
            SaveFileName = "my-save-file.txt",
            EncryptionSecret = "my-secret"
            // Elimina las propiedades que no existen (como AutosaveData y SaveFilepath)
        };

        // Pass it to FBPP
        FBPP.Start(config);
    }

    public static void DeleteAllkeys()
    {
        // Mueve este método fuera de Awake
        FBPP.DeleteAll();
        FBPP.Save();
    }
}
