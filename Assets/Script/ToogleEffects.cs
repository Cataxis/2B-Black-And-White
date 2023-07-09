using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToggleEffects : MonoBehaviour
{
    public bool effectsActive = false;
    
    public void OnEffectsToggleClicked()
    {
        if(effectsActive)
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "DESACTIVATE";
        }

        else
        {
            GetComponentInChildren<TextMeshProUGUI>().text = "ACTIVATE";
        }

        effectsActive = !effectsActive;
    }
}