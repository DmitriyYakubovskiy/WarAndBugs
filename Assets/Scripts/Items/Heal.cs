using UnityEngine;

public class Heal : Item
{
    [SerializeField] private int procentHp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            var entity = collision.gameObject.GetComponent<Entity>();
            if(entity.Lives==entity.hp.GetComponent<HealthBar>().MaxHealth) return;
            PlaySound(0, volume, isDestroyed:true);
            entity.Lives+=entity.hp.GetComponent<HealthBar>().MaxHealth *procentHp/100;
            Destroy(gameObject);
        }
    }
}
