using UnityEngine;

public class GrenadeItem : Item
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().GetCountGrenade()< collision.gameObject.GetComponent<Player>().GetMaxCountGrenade())
            {
                PlaySound(0, volume, isDestroyed: true);
                collision.gameObject.GetComponent<Player>().AddGrenade();
                Destroy(gameObject);
            }
        }
    }
}
