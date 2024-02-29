using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GrenadeShell : Sound
{
    [SerializeField] private GameObject BlowUpSprite;
    [SerializeField] private float speed;
    [SerializeField] private float timeBlownUp;
    private List<Collider2D> collisions;
    private Vector2 targetPosition;
    private float distanceThreshold = 0.1f;
    private bool isBlownUp = false;

    public float radius { get; set; }
    public float damage { get; set; }

    private void Awake()
    {
        collisions = new List<Collider2D>();
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void Start()
    {
        gameObject.GetComponent<CapsuleCollider2D>().size = new Vector2(radius, radius);
        BlowUpSprite.GetComponent<Transform>().localScale = new Vector2(radius, radius);
    }

    private void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
        transform.eulerAngles += new Vector3(0, 0, 180)*speed*Time.deltaTime;
        if (Vector2.Distance(transform.position, targetPosition) <= distanceThreshold && isBlownUp==false)
        {
            ExplodeGrenade();
        }
    }

    private void ExplodeGrenade()
    {
        BlowUpSprite.SetActive(true);
        foreach (var collision in collisions.ToList())
        {
            collision.gameObject.GetComponent<Entity>()?.TakeDamage(damage);
        }
        isBlownUp=true;
        PlaySound(0, volume, isDestroyed:true);
        Invoke("DisanableBlowUpSprite", timeBlownUp);
    }

    private void DisanableBlowUpSprite()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Enemy") collisions.Add(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy") collisions.Remove(collision);
    }
}
