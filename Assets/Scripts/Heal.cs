using UnityEngine;

public class Heal : Sound
{
    [SerializeField] private int procentHp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            PlaySound(0,isDestroyed:true);
            var entity = collision.gameObject.GetComponent<Entity>();
            entity.Lives+=entity.hp.GetComponent<HealthBar>().MaxHealth *procentHp/100;
            Destroy(gameObject);
        }
    }
}
