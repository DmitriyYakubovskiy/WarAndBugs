using UnityEngine;

public class TemporaryFlyingBug : Bug
{
    [SerializeField] public int directionX;
    [SerializeField] public int directionY;

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
            moveVector.x = directionX;
            moveVector.y = directionY;
        }
        PlayerPosition();
        Move(true);
        hp.ShowHealth(this);
    }
}
