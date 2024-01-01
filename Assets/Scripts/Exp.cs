using UnityEngine;

public class Exp : Sound
{
    [SerializeField] private Player player;
    [SerializeField] private float startTime=0.2f;
    [SerializeField] private float speed =5;
    private Rigidbody2D rigidbody=>GetComponent<Rigidbody2D>();
    private Vector2 moveVector;
    private float time;
    private bool check = false;
    public float exp;

    private void Awake()
    {
        time = startTime;
    }

    private void Update()
    {
        if (check)
        {
            if (time > 0)
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
                transform.position =Vector3.Lerp(transform.position,player.transform.position,(speed*player.KSpeed*player.KSpeedFromWeapon)*Time.deltaTime);
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
        if (time > 0) time -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (check)
            {
                ////PlaySound(0);
                player.Exp += exp;
                Destroy(gameObject);
            }
            player = collision.GetComponent<Player>();
            check=true;
        }
    }
}
