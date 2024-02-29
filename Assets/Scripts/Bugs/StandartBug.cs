using UnityEngine;

public class StandartBug : Bug
{
    private void Update()
    {
        Reposition();
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
        Move(true);
        hp.ShowHealth(this);
    }
}
