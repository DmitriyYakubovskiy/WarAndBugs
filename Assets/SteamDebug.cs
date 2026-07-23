using UnityEngine;

public class SteamDebug : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($"Steam init: {SteamManager.Initialized}");
    }
}
