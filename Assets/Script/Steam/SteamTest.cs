using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SteamTest : MonoBehaviour
{
    public bool notConnected;
   
    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(3109440);
            PrintYourName();
        }
        catch (System.Exception e)
        {
            notConnected = true;
            Debug.Log(e);
        }
    }

    private void FixedUpdate()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void PrintYourName()
    {
        Debug.Log(Steamworks.SteamClient.Name);
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }
}
