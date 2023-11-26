using UnityEngine;

public class DamageUp : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.KDamage *= 1.1f;
            gameObject.SetActive(false);
        }
    }
}
