using UnityEngine;

public abstract class Bug : Entity
{
    [SerializeField] protected int exp;
    [SerializeField] protected float damage;
    [SerializeField] protected float startTimeAttack;
    [SerializeField] protected float timeAttackAnimation;

    protected GameObject playerForAttack = null;
    protected GameObject player;
    protected Vector2 playerPosition;
    protected float timeAttack;
    protected float startTimeSearchPlayer= 0.2f;
    protected float timeSearchPlayer=0;
    protected float randomMoveTime = 0;
    protected bool randomMove = false;

    protected override void Awake()
    {
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
        if (GameObject.Find("Player") != null)
        {
            player = GameObject.Find("Player");
        }

        hp.MaxHealth = Lives;
        var random = new System.Random();
        startSpeed = speed;
        speed = (random.Next((int)((speed - speed / 4) * 100), (int)((speed + speed / 4) * 100))) / 100f;//+-25%
        materialHeal = Resources.Load("Materials/HealBlink", typeof(Material)) as Material;
        materialDamage = Resources.Load("Materials/DamageBlink", typeof(Material)) as Material;
        materialDefault = spriteRenderer.material; 
    }

    protected virtual void FixedUpdate()
    {
        Animator.speed = 1;
        if (isDead)
        {
            State = States.Dead;
        }
        else
        {
            if (timeAttack > 0 && timeAttack < startTimeAttack + timeAttackAnimation && startTimeAttack + timeAttackAnimation - timeAttack < timeAttackAnimation)
            {
                State = States.Attack;
            }
            else if (moveVector.x == 0 && moveVector.y == 0)
            {
                State = States.Idle;
            }
            else
            {
                SpeedCalculation();
                if (Animator.speed == 0) State = States.Idle;
                else State = States.Run;
            }
        }
        previousPosition = rigidbody.position;
    }

    protected void DistanceToPlayer()
    {
        if (timeSearchPlayer > 0) timeSearchPlayer -= Time.deltaTime;
        if (timeSearchPlayer <= 0)
        {
            playerPosition = player.transform.position;
            timeSearchPlayer = startTimeSearchPlayer;
        }
    }

    protected virtual void AiLogics()
    {
        if (randomMove && randomMoveTime > 0)
        {
            randomMoveTime -= Time.deltaTime;
            return;
        }
        if (randomMove && randomMoveTime <= 0)
        {
            var PlayerVector = playerPosition;
            var ThisPosition = transform.position;
            moveVector = new Vector2(0, 0);
            if (PlayerVector.x > ThisPosition.x + 0.5 || PlayerVector.x < ThisPosition.x - 0.5)
            {
                SetAngles(PlayerVector.x < ThisPosition.x);
            }
            return;
        }
        if (timeAttack < startTimeAttack && timeAttack > 0)
        {
            moveVector = new Vector2(0, 0);
            return;
        }
        if (playerForAttack == null)
        {
            var PlayerVector = playerPosition;
            var ThisPosition = transform.position;
            if (PlayerVector.x > ThisPosition.x + 0.2 || PlayerVector.x < ThisPosition.x - 0.2)
            {
                moveVector.x = PlayerVector.x > ThisPosition.x ? 1 : -1;
            }
            else
            {
                moveVector.x = 0;
            }
            if (PlayerVector.y > ThisPosition.y + 0.2 || PlayerVector.y < ThisPosition.y - 0.2)
            {
                moveVector.y = PlayerVector.y > ThisPosition.y ? 1 : -1;
            }
            else
            {
                moveVector.y = 0;
            }
            return;
        }
    }

    //protected virtual void SpeedCalculation()
    //{
    //    double v = Math.Sqrt(Math.Pow(rigidbody.position.x - previousPosition.x,2) + Math.Pow(rigidbody.position.y - previousPosition.y, 2));
    //    var k = Math.Abs(v / (speed * Time.deltaTime));
    //    Animator.speed = (float)k;
    //}

    protected virtual void RechargeTimeAttack()
    {
        if (timeAttack > 0) timeAttack -= Time.deltaTime;
        else
        {
            randomMoveTime = 0;
            randomMove = false;
        }
    }

    protected virtual void Attack()
    {
        if (playerForAttack != null)
        {
            if (timeAttack <= 0)
            {
                Invoke("GetDamage", timeAttackAnimation/2);
                timeAttack = startTimeAttack + timeAttackAnimation;
            }
        }
    }

    public virtual void GetDamage()
    {
        PlaySound(0);
        if (playerForAttack != null)
        {   
            playerForAttack.GetComponent<Player>().TakeDamage(damage);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerForAttack = collision.gameObject;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerForAttack = null;
        }
    }

    protected virtual void OnDestroy()
    {
        if (player != null)
        {
            player.GetComponent<Player>().Exp+=exp;
            player.GetComponent<Player>().Kills++;
        }
    }
}
