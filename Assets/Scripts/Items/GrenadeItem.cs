using UnityEngine;

public class GrenadeItem : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //PlaySound(0, isDestroyed: true);
            if (!collision.gameObject.GetComponent<Player>().GrenadeGetActive())
            {
                collision.gameObject.GetComponent<Player>().GrenadeSetActive(true);
                Destroy(gameObject);
            }
        }
    }
}
