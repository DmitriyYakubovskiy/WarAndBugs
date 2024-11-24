using UnityEngine;

public class DamageUp : Up
{
    private void Start()
    {
        UpgradesName = "DamageUp";
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.KDamage += k / 100;
            gameObject.SetActive(false);
        }
    }
}
