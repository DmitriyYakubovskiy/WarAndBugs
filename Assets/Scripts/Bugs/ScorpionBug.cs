using UnityEngine;
using UnityEngine.UI;

public class ScorpionBug : Bug
{
    [SerializeField] private GameObject transformPoint;
    [SerializeField] private GameObject bullet;

    private void Update()
    {
        if (isDead)
        {
            RechargeTimeDead();
            moveVector = new UnityEngine.Vector2(0, 0);
        }
        else
        {
            Attack();
            RechargeTimeAttack();
            AiLogics();
        }
        DistanceToPlayer();
        MakeRotation();
        Move(true);
        hp.ShowHealth(this);
    }

    public override void GetDamage()
    {
        bullet.GetComponent<Bullet>().TimeDieBullet = 6;
        Instantiate(bullet, transformPoint.transform.position, transformPoint.transform.rotation);
        System.Random random = new System.Random();
        randomMoveTime = random.Next(10, (int)startTimeAttack * 95)/100;
        moveVector.x = random.Next(-1, 2);
        moveVector.y = random.Next(-1, 2);
        randomMove = true;
    }

    protected override void Attack()
    {
        if (playerForAttack != null)
        {
            if (timeAttack <= 0)
            {
                PlaySound(0);
                Invoke("GetDamage", timeAttackAnimation - 0.25f);
                timeAttack = startTimeAttack + timeAttackAnimation - 0.25f;
            }
        }
    }

    protected virtual void MakeRotation()
    {
        if (player != null)
        {
            var dir = player.transform.position - transformPoint.transform.position;
            float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transformPoint.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
        }
    }
}
