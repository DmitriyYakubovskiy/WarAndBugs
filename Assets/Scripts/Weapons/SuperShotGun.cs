using UnityEngine;

public class SuperShotGun : ShotGun
{
    [SerializeField] private float startMinorLoadTime;
    [SerializeField] private int countsShotWithoutReload;
    private int countShot = 0;

    protected override void Shot()
    {
        if (time <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                PlaySound(0, volume);
                WeaponFire.SetActive(true);
                Invoke("DisanabledWeaponFire", 0.1f < startMainReloadTime ? 0.1f : startMainReloadTime / 2);
                GameObject[] bullets = new GameObject[countBullets];
                for (int i = 0; i < countBullets; i++)
                {
                    bullets[i] = Instantiate(bullet, transformPoint.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + countBullets / 2 - accuracy * i)));
                    bullets[i].GetComponent<Bullet>().damage *= player.KDamage;
                }
                countShot++;
                if (countShot % countsShotWithoutReload == 0) time = startMainReloadTime / player.KReload;
                else time = startMinorLoadTime / player.KReload;
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
