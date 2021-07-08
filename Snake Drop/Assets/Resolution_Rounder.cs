using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resolution_Rounder : MonoBehaviour
{
    private float lastX;
    private float lastY;

    private void Awake()
    {
        lastX = Screen.width;
        lastY = Screen.height;
    }

    private void LateUpdate()
    {
        if(Screen.width != lastX || Screen.height != lastY) RoundScreenSize();
    }

    private void RoundScreenSize()
    {
        int currentX = Screen.width;
        int currentY = Screen.height;

        int Xdiff = currentX % 2;
        int Ydiff = currentY % 2;

        if (Xdiff > 0 || Ydiff > 0)
        {
            Screen.SetResolution(currentX - Xdiff, currentY - Ydiff, Screen.fullScreenMode);
        }
    }
}
