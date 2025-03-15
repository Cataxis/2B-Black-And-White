using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Screenshoot : MonoBehaviour { 
private void Update()
{
    if (Input.GetKeyDown(KeyCode.Z))

        ScreenCapture.CaptureScreenshot("screenshot-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".png", 4);

}
}
