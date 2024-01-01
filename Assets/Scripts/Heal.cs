using UnityEngine;

public class Heal : Sound
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            //PlaySound(0);
            var entity = collision.gameObject.GetComponent<Entity>();
            entity.Lives+=entity.hp.GetComponent<HealthBar>().MaxHealth / 4;
            Destroy(gameObject);
        }
    }
}
