using UnityEngine;

public class BoomBullet : Bullet
{
    [SerializeField] private GameObject BlowUp;
    [SerializeField] private float timeBlownUp;
    [SerializeField] private float radius;

    protected virtual void Start()
    {

    }

    protected override void DealDamage(Collider2D collision)
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var item in collisions)
        {
            if(item.gameObject.tag == "Enemy" || item.gameObject.tag == "DestrItem")
                item.gameObject.GetComponent<Entity>()?.TakeDamage(damage);
        }
        var obj = Instantiate(BlowUp, transform.position, Quaternion.identity);
        obj.GetComponent<Transform>().localScale = new Vector2(radius, radius);
        Destroy(obj, timeBlownUp);
        PlaySound(0, volume * 3, isDestroyed: true);
        Invoke("DisenableBlowUp", timeBlownUp);
        Destroy(gameObject);
    }

    private void DisenableBlowUp()
    {
        Destroy(gameObject);
    }
}