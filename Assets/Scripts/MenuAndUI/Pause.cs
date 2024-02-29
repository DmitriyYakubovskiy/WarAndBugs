using UnityEngine;

public static class Pause
{
    public static bool IsPaused { get; set; } = false;
    public static void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            AudioSource[] audioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            for(int i=0;i<audioSources.Length;i++)
            {
                if (audioSources[i].isPlaying) audioSources[i].Pause();
            }
            Time.timeScale = 0f;
        }
        IsPaused = true;
    }

    public static void ContinueGame()
    {
        if (Time.timeScale != 1)
        {
            AudioSource[] audioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
            for (int i = 0; i < audioSources.Length; i++)
            {
                if (!audioSources[i].isPlaying) audioSources[i].UnPause();
            }
            Time.timeScale = 1f;
        }
        IsPaused = false;
    }
}
