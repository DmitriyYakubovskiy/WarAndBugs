using UnityEngine;

public class Weapon : Sound
{
    [SerializeField] protected Camera camera;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject WeaponFire;
    [SerializeField] protected Transform transformPoint;
    [SerializeField] protected Player player;
    [SerializeField] protected float startTime;

    protected SpriteRenderer spriteRenderer;

    protected float time;

    protected virtual void Awake()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.tag == "Sprite")
            {
                spriteRenderer = child.GetComponent<SpriteRenderer>();
                break;
            }
        }
    }

    protected virtual void Update()
    {
        if (Time.timeScale != 0)
        {
            MakeRotation();
            Shoot();
        }
    }

    protected virtual void MakeRotation()
    {
        var dir = camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rotationY = transform.eulerAngles.y;
        if (Mathf.Abs(rotationZ) >= 90 && rotationY == 0) 
        {
            rotationY = 180;
            transform.eulerAngles = new Vector3 (0, rotationY, 180 -rotationZ);
        }
        if (Mathf.Abs(rotationZ) < 90 && rotationY == 180)
        {
            rotationY = 0;
            transform.eulerAngles = new Vector3(0, rotationY, -rotationZ);
        }
        if (rotationY == 180)
        {
            transform.eulerAngles = new Vector3(0, rotationY, 180 - rotationZ);
        }        
        if (rotationY == 0)
        {
            transform.eulerAngles = new Vector3(0, rotationY, rotationZ);
        }
    }

    protected virtual void DisanabledWeaponFire()
    {
        WeaponFire.SetActive(false);
    }

    protected virtual void Shoot()
    {
        if (time <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                PlaySound(0,0.15f,0.7f,1f);
                WeaponFire.SetActive(true);
                Invoke("DisanabledWeaponFire", 0.1f);
                var bulletTmp=Instantiate(bullet,transformPoint.position,transform.rotation);
                bulletTmp.GetComponent<Bullet>().damage *=player.KDamage;
                time = startTime/player.KReload;
            }
        }
        else
        {
            time-= Time.deltaTime;
        }
    }
}
