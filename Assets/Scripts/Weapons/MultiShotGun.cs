using UnityEngine;

public class MultiShotGun : Weapon
{
    [SerializeField] private float startTimeAttack;
    [SerializeField] private float delay;

    private float timeDelay=0;

    protected override void Shot()
    {
        if (CanShot())
        {
            RepitShot();
            return;
        }
    }

    private bool CanShot()
    {
        time -= Time.deltaTime;
        if (time > startMainReloadTime-startTimeAttack)
        {
            AudioStart(0); 
            WeaponFire.SetActive(true);
            if (Input.GetMouseButton(0) && timeDelay <= 0)
            {
                timeDelay = delay / player.KReload; 
                return true;
            }
            else
            {
                timeDelay -= Time.deltaTime;
            }
        }
        else
        {
            AudioStop();
            WeaponFire.SetActive(false);
        }
        if (time <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                time = startMainReloadTime / player.KReload;
                return true;
            }
        }
        return false;
    }

    private void RepitShot()
    {
        var bulletTmp = Instantiate(bullet, transformPoint.position, transform.rotation);
        bulletTmp.GetComponent<Bullet>().damage *= player.KDamage;
    }
}
