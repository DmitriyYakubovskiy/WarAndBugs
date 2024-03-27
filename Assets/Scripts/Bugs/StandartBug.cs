using UnityEngine;

public class StandartBug : Bug
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
}
