using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour
{
    public void ChangeScenes(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }

    public void RestartScene()
    {
        ChangeScenes(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        ChangeScenes((SceneManager.GetActiveScene().buildIndex) + 1);
    }

    public static void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }

    public static void Restart()
    {
        ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }
}
