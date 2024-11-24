using UnityEngine;

public class MultiGun : Weapon
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
        float buf = startMainReloadTime < startTimeAttack ? startTimeAttack + 0.1f : startMainReloadTime / player.KReload;
        time -= Time.deltaTime;
        if (time > buf - startTimeAttack)
        {
            AudioStart(0,volume); 
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
                time = buf;
                return true;
            }
        }
        return false;
    }

    private void RepitShot()
    {
        float accuracyTmp = Random.Range((int)(-accuracy * 100), (int)(accuracy * 100)) / 100;
        var bulletTmp = Instantiate(bullet, transformPoint.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - accuracyTmp)));
        bulletTmp.GetComponent<Bullet>().damage *= player.KDamage;
    }
}
