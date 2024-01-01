using System;
using UnityEngine;

public class Entity : Sound
{   
    [SerializeField] public HealthBar hp; 
    [SerializeField] protected float speed;
    [SerializeField] protected float lives;
    [SerializeField] protected float timeDeadAnimation;
    protected Animator Animator { get; set; }
    protected SpriteRenderer spriteRenderer;
    protected Transform transform;
    protected Rigidbody2D rigidbody;
    protected Material materialHeal;
    protected Material materialDamage;
    protected Material materialDefault;
    protected Vector2 moveVector;
    protected Vector2 previousPosition;
    protected float startSpeed;
    protected bool isDead=false;

    public float Lives
    {
        get => lives;
        set
        {
            if (value > hp.MaxHealth) value = hp.MaxHealth;
            lives = value;
            spriteRenderer.material = materialHeal;
            Invoke("ResetDamageMaterial", 0.1f);
            hp.ShowHealth(this);
        }
    }

    public bool IsFlip { get; set; }

    protected States State
    {
        get { return (States)Animator.GetInteger("State"); }
        set { Animator.SetInteger("State", (int)value); }
    }

    protected virtual void Awake()
    {
        startSpeed = speed;
        Animator = GetComponentInChildren<Animator>();
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Sprite")
            {
                spriteRenderer = child.GetComponent<SpriteRenderer>();
                break;
            }
        }

        materialHeal = Resources.Load("Materials/HealBlink", typeof(Material)) as Material;
        materialDamage = Resources.Load("Materials/DamageBlink", typeof(Material)) as Material;
        materialDefault = spriteRenderer.material;
    }

    public virtual void TakeDamage(float damage)
    {
        if (lives <= 0) return;
        lives -= damage;
        spriteRenderer.material = materialDamage;

        if (lives <= 0)
        {
            lives = 0;
            isDead = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            ResetDamageMaterial();
            PlaySound(2);
        }
        else
        {
            PlaySound(1);
            Invoke("ResetDamageMaterial", 0.1f);
        }
    }

    public void ResetDamageMaterial()
    {
        spriteRenderer.material = materialDefault;
    }

    protected virtual void SpeedCalculation()
    {
        double v = Math.Sqrt(Math.Pow(rigidbody.position.x - previousPosition.x, 2) + Math.Pow(rigidbody.position.y - previousPosition.y, 2));
        var k = Math.Abs(v / (startSpeed * Time.deltaTime));
        Animator.speed = (float)k;
    }

    protected void SetFlip(bool flip)
    {
        IsFlip = flip;
        spriteRenderer.flipX = flip;

    }

    protected void SetAngles(bool flip)
    {
        IsFlip = flip;
        if (IsFlip) transform.eulerAngles = new Vector2(0, 180);
        else transform.eulerAngles = new Vector2(0, 0);
    }

    protected virtual void Move(bool b)
    {
        if (moveVector.x < 0 && b==false) SetFlip(true);
        else if (moveVector.x > 0 && b == false) SetFlip(false);
        if (moveVector.x < 0 && b == true) SetAngles(true);
        else if (moveVector.x > 0 && b == true) SetAngles(false);

        moveVector = moveVector.normalized;
        moveVector = moveVector * speed;
        rigidbody.velocity = moveVector;
    }

    protected void RechargeTimeDead()
    {
        if(!isDead) return;
        if (timeDeadAnimation > 0) timeDeadAnimation -= Time.deltaTime;
        else Destroy(gameObject);
    }
}
