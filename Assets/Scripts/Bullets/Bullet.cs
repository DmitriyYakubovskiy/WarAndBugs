using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected GameObject damageEffect;
    [SerializeField] protected GameObject bloodSplash;
    [SerializeField] protected string[] tags;
    [SerializeField] protected float timeDieBullet=2;
    [SerializeField] private int penetration=1;

    public float speed;
    public float damage;

    public float TimeDieBullet { get=>timeDieBullet; set => timeDieBullet=value; }

    protected virtual void Update()
    {
        Move();
        if(timeDieBullet <= 0) Destroy(gameObject);
        timeDieBullet-= Time.deltaTime;
    }

    protected virtual void Move()
    {
        transform.Translate(Vector2.right*speed*Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger == false || collision.layerOverridePriority==1)
        {
            if (Array.IndexOf(tags,collision.gameObject.tag)!=-1)
            {
                collision.gameObject.GetComponent<Entity>().TakeDamage(damage);
                penetration -= 1;
                if(penetration<=0) Destroy(gameObject);
            }
            if (collision.gameObject.tag == "Ground")
            {
                Destroy(gameObject);
            }
        }
    }
}
