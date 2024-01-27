using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Pause
{
    public static void PauseGame()
    {
        if (Time.timeScale!=0) Time.timeScale = 0f;
    }

    public static void ContinueGame()
    {
        if (Time.timeScale != 1) Time.timeScale= 1f;
    }
}
