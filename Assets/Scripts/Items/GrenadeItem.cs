using UnityEngine;

public class GrenadeItem : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!collision.gameObject.GetComponent<Player>().GrenadeGetActive())
            {
                PlaySound(0, volume, isDestroyed: true);
                collision.gameObject.GetComponent<Player>().GrenadeSetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
