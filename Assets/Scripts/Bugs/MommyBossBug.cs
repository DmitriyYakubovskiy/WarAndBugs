using System.Collections.Generic;
using UnityEngine;

public class MommyBossBug : Bug
{
    [SerializeField] private float startTimeSpawnBugs;
    [SerializeField] private float startTimeMegaAttack;
    [SerializeField] private float startTimeDownAttack;
    [SerializeField] private float timeMegaAttackAnimation;
    [SerializeField] private float timeDownAttackAnimation;
    [SerializeField] private float timeSpawnBugsAnimation;
    [SerializeField] private GameObject spawnSystem;
    [SerializeField] private float regeniration;
    private int currentAttackId = 0;
    private float timeSpawn=0;
    private Dictionary<int, float> startTimeAttacks;
    private Dictionary<int, float> timeAnimations;
    


    protected virtual void Awake()
    {
        base.Awake();
        if (GameObject.Find("SpawnSystem") != null)
        {
            spawnSystem = GameObject.Find("SpawnSystem");
        }
        startTimeAttacks = new Dictionary<int, float>();    
        startTimeAttacks.Add(0, startTimeAttack);
        startTimeAttacks.Add(1, startTimeMegaAttack);
        startTimeAttacks.Add(2, startTimeDownAttack);

        timeAnimations = new Dictionary<int, float>();
        timeAnimations.Add(0, timeAttackAnimation);
        timeAnimations.Add(1, timeMegaAttackAnimation);
        timeAnimations.Add(2, timeDownAttackAnimation);
    }

    private void Update()
    {
        if (isReplaced) Reposition();
        if (isTemporary) RechargeTimeLive();
        if (isDead)
        {
            RechargeTimeDead();
            moveVector = new UnityEngine.Vector2(0, 0);
        }
        else
        {
            Lives += regeniration * Time.deltaTime;
            Attack();
            RechargeTimeAttack();
            AiLogics();
        }
        PlayerPosition();
        Move(true);
        hp.ShowHealth(this);
    }

    protected override void FixedUpdate()
    {
        Animator.speed = 1;
        if (isDead)
        {
            State = States.Dead;
        }
        else
        {
            if (timeAttack > 0 && 
                timeAttack < startTimeAttacks[currentAttackId] + timeAnimations[currentAttackId] && 
                startTimeAttacks[currentAttackId] - timeAttack < 0)
            {
                if (currentAttackId == 0) State = States.Attack1;
                if (currentAttackId == 1) State = States.Attack2;
                if (currentAttackId == 2) State = States.Attack4;
            }
            else if (timeSpawn>0 &&
                timeSpawn<startTimeSpawnBugs + timeSpawnBugsAnimation &&
                startTimeSpawnBugs - timeSpawn < 0)
            {
                State = States.Attack3;
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

    protected override void AiLogics()
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
        if (DistanceToPlayer()<0.2f)
        {
            moveVector = new Vector2(0, 0);
            return;
        }
        if (timeAttack < startTimeAttacks[currentAttackId] && timeAttack > 0)
        {
            moveVector = new Vector2(0, 0);
            return;
        }
        if (timeSpawn > startTimeSpawnBugs - timeSpawnBugsAnimation)
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

    protected override void Attack()
    {
        if (playerForAttack != null)
        {
            if (timeAttack <= 0)
            {
                currentAttackId = Random.Range(0, 3);
                if (transform.position.y > playerForAttack.transform.position.y)
                {
                    currentAttackId = 2;
                    Invoke("GetDamage", timeDownAttackAnimation / 1.2f);
                    timeAttack = startTimeDownAttack + timeDownAttackAnimation;
                    return;
                }
                if (currentAttackId == 1)
                {
                    Invoke("GetDamage", timeMegaAttackAnimation / 3);
                    Invoke("GetDamage", timeMegaAttackAnimation / 2);
                    Invoke("GetDamage", timeMegaAttackAnimation / 1.2f);
                    timeAttack = startTimeMegaAttack + timeMegaAttackAnimation;
                }
                else
                {
                    currentAttackId = 0;
                    Invoke("GetDamage", timeAttackAnimation / 2);
                    Invoke("GetDamage", timeAttackAnimation / 2);
                    timeAttack = startTimeAttack + timeAttackAnimation;
                }
            }
        }
        else
        {
            if (timeSpawn <= 0)
            {
                SpawnBugs();
                timeSpawn = startTimeSpawnBugs + timeSpawnBugsAnimation;
            }
        }
    }

    protected override void RechargeTimeAttack()
    {
        if(timeSpawn > 0) timeSpawn-=Time.deltaTime;
        if (timeAttack > 0)
        {
            timeAttack -= Time.deltaTime;
        }
        else
        {
            randomMoveTime = 0;
            randomMove = false;
        }
    }

    public override void TakeDamage(float damage, bool bonus = true)
    {
        if (lives > 0 && lives - damage <= 0)
        {
            spawnSystem.GetComponent<SpawnSystem>().IsBossStage = false;
            PlayerPrefs.SetInt("Flamethrower", 1);
        }
        base.TakeDamage(damage, bonus);
    }

    public void SpawnBugs()
    {
        PlaySound(3,volume);
        spawnSystem.GetComponent<SpawnSystem>().SpawnEntities(7);
    }

    public override void GetDamage()
    {
        PlaySound(0, volume);
        if (playerForAttack != null) playerForAttack.GetComponent<Player>().TakeDamage(damage);
    }
}
