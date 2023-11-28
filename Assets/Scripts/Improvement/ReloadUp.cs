using UnityEngine;

public class ReloadUp : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float k;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.KReload += k / 100;
            gameObject.SetActive(false);
        }
    }
}
