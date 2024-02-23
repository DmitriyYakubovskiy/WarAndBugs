using UnityEngine;

public class ReloadUp : Up
{
    private void Awake()
    {
        UpgradesName = "ReloadUp";
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.KReload += k / 100;
            gameObject.SetActive(false);
        }
    }
}
