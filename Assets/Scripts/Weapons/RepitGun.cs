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
                time = startMainReloadTime / player.KReload;
                timeDelay = delay / player.KReload;
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
        PlaySound(0, volume);
        WeaponFire.SetActive(true);
        Invoke("DisanabledWeaponFire", 0.1f);
        System.Random random = new System.Random();
        float accuracyTmp = random.Next((int)(-accuracy * 100), (int)(accuracy * 100)) / 100;
        var bulletTmp = Instantiate(bullet, transformPoint.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - accuracyTmp)));
        bulletTmp.GetComponent<Bullet>().damage *= player.KDamage;
    }
}
