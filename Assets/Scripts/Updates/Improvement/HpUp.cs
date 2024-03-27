public class HpUp : Up
{
    private void Awake()
    {
        UpgradesName = "HpUp";
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.Lives=player.hp.GetComponent<HealthBar>().MaxHealth;
            player.hp.GetComponent<HealthBar>().MaxHealth=player.Lives * (1 + k / 100);
            player.Lives = player.Lives * (1 + k / 100);
            gameObject.SetActive(false);
        }
    }
}
