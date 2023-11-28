using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float k;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.KSpeed += k/100;
            gameObject.SetActive(false);
        }
    }
}
