using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 90;

        var config = new FBPPConfig()
        {
            // Aseg�rate de usar propiedades v�lidas para FBPPConfig
            SaveFileName = "my-save-file.txt",
            EncryptionSecret = "my-secret"
            // Elimina las propiedades que no existen (como AutosaveData y SaveFilepath)
        };

        // Pass it to FBPP
        FBPP.Start(config);
    }

    public static void DeleteAllkeys()
    {
        // Mueve este m�todo fuera de Awake
        FBPP.DeleteAll();
        FBPP.Save();
    }
}
