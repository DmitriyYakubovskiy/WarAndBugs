public class SpeedUp : Up
{
    private void Awake()
    {
        UpgradesName = "SpeedUp";
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.KSpeed += k / 100;
            gameObject.SetActive(false);
        }
    }
}
