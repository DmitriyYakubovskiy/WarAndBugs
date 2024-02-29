using System;
using System.Linq;
using UnityEngine;

public class Drone : Sound
{
    [SerializeField] private GameObject gunPoint;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject WeaponFire;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float startTimeAttack=0;
    [SerializeField] private float speed;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody;
    private GameObject player;
    private GameObject currentBug;
    private Transform transform;
    private Vector2 moveVector;
    private Vector2 playerPosition;
    private float startTimeSearchBug = 0.2f;
    private float timeSearchBug = 0;
    private float timeAttack=0;
    private float kDamage = 1;

    private Animator Animator { get; set; }
    public bool IsFlip { get; set; }

    private States State
    {
        get { return (States)Animator.GetInteger("State"); }
        set { Animator.SetInteger("State", (int)value); }
    }

    private void Awake()
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
        SearchPlayer();
        SearchBug();
        if (PlayerPrefs.GetInt("Drone") == 0) gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("Drone"))
        {
            int level = PlayerPrefs.GetInt("Drone");
            for (int i = 0; i < level; i++)
            {
                startTimeAttack *= 0.75f;
                kDamage *= 1.05f;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Drone", 0);
        }
    }

    private void Update()
    {
        SearchBug();
        MakeRotation();
        Attack();

        AiLogics();

        DistanceToBug();
        RechargeTimeAttack();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void SearchPlayer()
    {
        if (player == null) player = GameObject.Find("Player");
    }

    private void SearchBug()
    {
        if (currentBug == null) currentBug = GameObject.FindGameObjectsWithTag("Enemy").FirstOrDefault();
    }

    private void MakeRotation()
    {
        if (currentBug == null) return;
        var dir = currentBug.transform.position - gunPoint.transform.position;
        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rotationY = gunPoint.transform.eulerAngles.y;
        if (Mathf.Abs(rotationZ) >= 90 && rotationY == 0)
        {
            rotationY = 180;
            gunPoint.transform.eulerAngles = new Vector3(0, rotationY, 180 - rotationZ);
        }
        if (Mathf.Abs(rotationZ) < 90 && rotationY == 180)
        {
            rotationY = 0;
            gunPoint.transform.eulerAngles = new Vector3(0, rotationY, -rotationZ);
        }
        if (rotationY == 180)
        {
            gunPoint.transform.eulerAngles = new Vector3(0, rotationY, 180 - rotationZ);
        }
        if (rotationY == 0)
        {
            gunPoint.transform.eulerAngles = new Vector3(0, rotationY, rotationZ);
        }
    }

    private void Attack()
    {
        if (currentBug?.GetComponent<Entity>().isDead == true)
        {
            SearchBug();
            return;
        }
        if (currentBug != null)
        {
            if (timeAttack <= 0)
            {
                PlaySound(0, volume);
                var obj=Instantiate(bullet, point.transform.position, point.transform.rotation);
                obj.GetComponent<Bullet>().damage *= kDamage;
                WeaponFire.SetActive(true);
                Invoke("DisanabledWeaponFire", 0.1f);
                timeAttack = startTimeAttack;
            }
        }
    }

    private void Move()
    {
        moveVector = moveVector.normalized;
        moveVector = moveVector * speed;
        rigidbody.AddForce(moveVector,ForceMode2D.Force);
    }

    private void AiLogics()
    {
        if (player != null)
        {
            var PlayerVector = playerPosition;
            var ThisPosition = transform.position;
            if (PlayerVector.x > ThisPosition.x + 3 || PlayerVector.x < ThisPosition.x - 3)
            {
                moveVector.x = PlayerVector.x > ThisPosition.x ? 1 : -1;
            }
            else
            {
                moveVector.x = 0;
            }
            if (PlayerVector.y > ThisPosition.y + 1.5f || PlayerVector.y < ThisPosition.y - 1.5f)
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
    private void DistanceToBug()
    {
        if (currentBug != null)
        {
            if (timeSearchBug > 0) timeSearchBug -= Time.deltaTime;
            if (timeSearchBug <= 0)
            {
                if (player != null)
                {
                    playerPosition = player.transform.position;
                    timeSearchBug = startTimeSearchBug;
                }
            }
        }
    }

    private void RechargeTimeAttack()
    {
        if (timeAttack > 0) timeAttack -= Time.deltaTime;
    }

    private void DisanabledWeaponFire()
    {
        WeaponFire.SetActive(false);
    }
}
