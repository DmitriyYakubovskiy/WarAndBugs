using UnityEngine;

public class ShootGun : Weapon
{
    [SerializeField] private int countBullets;
    [SerializeField] protected float accuracy;

    protected override void Shot()
    {
        if (time <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                PlaySound(0);
                WeaponFire.SetActive(true);
                Invoke("DisanabledWeaponFire", 0.1f);
                GameObject[] bullets=new GameObject[countBullets];
                for (int i = 0; i < countBullets; i++)
                {
                    bullets[i]=Instantiate(bullet, transformPoint.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z +countBullets/2- accuracy*i)));
                    bullets[i].GetComponent<Bullet>().damage *= player.KDamage;
                }
                time = startTime / player.KReload;
            }
        }
        else
        {
            time -= Time.deltaTime;
        }
    }
}
