using UnityEngine;

public class Weapon : Sound
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject WeaponFire;
    [SerializeField] protected Transform transformPoint;
    [SerializeField] protected Player player;
    [SerializeField] protected string namesGun;
    [SerializeField] protected float startMainReloadTime;
    [SerializeField] protected float KSpeedPlayer=1;
    [SerializeField] protected float accuracy=0;

    protected SpriteRenderer spriteRenderer;
    protected bool flip=false;
    protected float time;
    

    protected virtual void Awake()
    {
        if (!PlayerPrefs.HasKey(namesGun))
        {
            if (namesGun == "Gun") PlayerPrefs.SetInt(namesGun, 1);
            else PlayerPrefs.SetInt(namesGun, 0);
        }
        if (!PlayerPrefs.HasKey("selectedGun")) PlayerPrefs.SetString("selectedGun", "Gun");
        if (PlayerPrefs.GetString("selectedGun") != namesGun) gameObject.SetActive(false);
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
        if (gameObject.activeSelf) player.KSpeedFromWeapon = KSpeedPlayer;
        if (Time.timeScale != 0)
        {
            MakeRotation();
            Shot();
        }
    }

    protected virtual void MakeRotation()
    {
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
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

    protected virtual void Shot()
    {
        if (time <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                PlaySound(0, volume);
                WeaponFire.SetActive(true);
                Invoke("DisanabledWeaponFire", 0.1f);
                System.Random random = new System.Random();
                float accuracyTmp=random.Next((int)(-accuracy*100),(int)(accuracy*100))/100;
                var bulletTmp= Instantiate(bullet, transformPoint.position, Quaternion.Euler(new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - accuracyTmp)));
                bulletTmp.GetComponent<Bullet>().damage *=player.KDamage;
                time = startMainReloadTime/player.KReload;
            }
        }
        else
        {
            time-= Time.deltaTime;
        }
    }
}
