using UnityEngine;

public class BugsRepair : MonoBehaviour
{
    private string key = "FirstLaunch";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(key) || PlayerPrefs.GetInt(key)==0)
        {
            PlayerPrefs.SetInt(key, 1);
            Scenes.Restart();
        }
    }
}
