using UnityEngine;

public class HpUp : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.Lives=player.hp.GetComponent<HealthBar>().MaxHealth;
            player.hp.GetComponent<HealthBar>().MaxHealth=player.Lives * 1.1f;
            player.Lives = player.Lives * 1.1f;
            gameObject.SetActive(false);
        }
    }
}
