using UnityEngine;

public static class Pause
{
    public static bool IsPaused { get; set; } = false;
    public static void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0f;
        }
        IsPaused = true;
    }

    public static void ContinueGame()
    {
        if (Time.timeScale != 1)
        {
            Time.timeScale = 1f;
        }
        IsPaused = false;
    }
}
