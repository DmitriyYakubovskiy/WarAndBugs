using UnityEngine;

public class RepitGun : Weapon
{
    [SerializeField] private int countBullets;
    [SerializeField] private float delay;

    private int currentCount = 0;
    private float timeDelay=0;

    protected override void Shot()
    {
        if (time <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                RepitShot();
                time = startTime / player.KReload;
                currentCount++;
                return;
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
        if (timeDelay <= 0)
        {
            if (currentCount <= countBullets && currentCount != 0)
            {
                RepitShot();
                timeDelay = delay / player.KReload;
                currentCount++;
                if (currentCount > countBullets) currentCount = 0;
            }
        }
        else
        {
            timeDelay -= Time.deltaTime;
        }
    }

    private void RepitShot()
    {
        PlaySound(0, 0.2f);
        WeaponFire.SetActive(true);
        Invoke("DisanabledWeaponFire", 0.1f);
        var bulletTmp = Instantiate(bullet, transformPoint.position, transform.rotation);
        bulletTmp.GetComponent<Bullet>().damage *= player.KDamage;
    }
}
