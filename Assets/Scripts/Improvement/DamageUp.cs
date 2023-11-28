using UnityEngine;

public class DamageUp : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float k;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.KDamage += k / 100;
            gameObject.SetActive(false);
        }
    }
}
