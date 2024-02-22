using UnityEngine;

public class GrenadeItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //PlaySound(0, isDestroyed: true);
            if (collision.gameObject.GetComponent<Player>().isActiveAndEnabled)
            {
                collision.gameObject.GetComponent<Player>().GrenadeSetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
