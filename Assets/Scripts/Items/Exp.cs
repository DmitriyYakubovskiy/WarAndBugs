using UnityEngine;

public class Exp : Item
{
    [SerializeField] private float speed = 5;
    private Rigidbody2D rigidbody=>GetComponent<Rigidbody2D>();
    private Vector2 moveVector;
    public float StartTime { get; set; } = 0.2f;
    public bool Check { get; set; } = false; 
    public float ExpCount { get; set; }
    public float Time { get; set; }

    protected override void Start()
    {
        base.Start();
        Time = StartTime;
    }

    private void Update()
    {
        base.Update();
        if (Check)
        {
            if (Time > 0)
            {
                var PlayerVector = player.transform.position;
                var ThisPosition = transform.position;
                if (PlayerVector.x > ThisPosition.x + 0.1f|| PlayerVector.x < ThisPosition.x - 0.1f)
                {
                    moveVector.x = PlayerVector.x > ThisPosition.x ? -1 : 1;
                }
                else
                {
                    moveVector.x = 0;
                }
                if (PlayerVector.y > ThisPosition.y + 0.1f || PlayerVector.y < ThisPosition.y-0.1f)
                {
                    moveVector.y = PlayerVector.y > ThisPosition.y ? -1 : 1;
                }
                else
                {
                    moveVector.y = 0;
                }
                
            }
            else
            {
                moveVector.x = 0; moveVector.y = 0;
                transform.position = Vector3.Lerp(transform.position, player.transform.position,(speed * player.KSpeed * player.KSpeedFromWeapon)* UnityEngine.Time.deltaTime);
            }
            Move();
            RechargeTime();
        }
    }

    private void Move()
    {
        moveVector = moveVector.normalized;
        moveVector = moveVector * speed;
        rigidbody.velocity = moveVector;
    }

    private void RechargeTime()
    {
        if (Time > 0) Time -= UnityEngine.Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Check)
            {
                PlaySound(0,volume,isDestroyed:true);
                player.Exp += ExpCount;
                Destroy(gameObject);
            }
            Check=true;
        }
    }
}
