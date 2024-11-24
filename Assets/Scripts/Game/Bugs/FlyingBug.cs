public class FlyingBug : Bug
{
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
            Attack();
            RechargeTimeAttack();
            AiLogics();
        }
        PlayerPosition();
        Move(true);
        hp.ShowHealth(this);
    }

    public override void GetDamage()
    {
        base.GetDamage();
        System.Random random = new System.Random();
        randomMoveTime = startTimeAttack;
        moveVector.x = random.Next(-1, 2);
        if (moveVector.x == 0) moveVector.x =moveVector.x+(random.Next(0,2)==0 ? 1 : -1);
        moveVector.y = random.Next(-1, 2);
        if (moveVector.y == 0) moveVector.y = moveVector.y + (random.Next(0, 2) == 0 ? 1 : -1);
        randomMove = true;
    }
}
