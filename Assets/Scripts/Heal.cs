using UnityEngine;

public class Heal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            var entity = collision.gameObject.GetComponent<Entity>();
            entity.Lives+=entity.hp.GetComponent<HealthBar>().MaxHealth / 2;
            Destroy(gameObject);
        }
    }
}
