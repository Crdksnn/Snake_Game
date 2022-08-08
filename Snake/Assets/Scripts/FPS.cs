using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public int targetFps;
    private bool lowFPS = false;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            lowFPS = !lowFPS;
            if(lowFPS)
                Application.targetFrameRate = targetFps;
            else
                Application.targetFrameRate = 120;
        }
    }
}
