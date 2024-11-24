using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Barrel : Entity
{
    [SerializeField] private GameObject BlownObject;
    [SerializeField] private float damage=100;
    [SerializeField] private float timeBlownUp = 1;
    private List<Entity> collisions = new List<Entity>();
    private bool isBlownUp = false;

    protected override void Start()
    {
        base.Start();
        hp.MaxHealth = Lives;
    }

    public override void TakeDamage(float damage, bool bonus = true)
    {
        if (Lives <= 0) return;
        Lives -= damage;

        if (Lives <= 0)
        {
            Lives = 0;
            isDead = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            PlaySound(2, volume);
            BlownUp();
            Destroy(this.gameObject);
        }
        else
        {
            PlaySound(1, volume);
        }
    }

    private void BlownUp()
    {
        foreach (var collision in collisions.ToList())
        {
            collision?.TakeDamage(damage);
        }
        isBlownUp = true;
        var obj = Instantiate(BlownObject, transform.position, Quaternion.identity);
        Destroy(obj, timeBlownUp);
        PlaySound(0, volume*12, isDestroyed: true);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "DestrItem") if (collision.GetComponent<Entity>())
            {
                collisions.Add(collision.GetComponent<Entity>());
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "DestrItem") collisions.Remove(collision.GetComponent<Entity>());
    }
}
