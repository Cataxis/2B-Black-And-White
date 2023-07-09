using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesactiveEffects : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.effectsActive = false;
    }
}
