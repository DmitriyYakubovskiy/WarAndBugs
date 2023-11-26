using UnityEngine;

public class ReloadUp : MonoBehaviour
{
    [SerializeField] private Player player;

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            player.KReload *= 1.1f;
            gameObject.SetActive(false);
        }
    }
}
