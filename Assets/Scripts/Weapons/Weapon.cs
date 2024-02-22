using Unity.VisualScripting;
using UnityEngine;

public class Weapon : Sound
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject WeaponFire;
    [SerializeField] protected Transform transformPoint;
    [SerializeField] protected Player player;
    [SerializeField] protected string name;
    [SerializeField] protected float startTime;
    [SerializeField] protected float KSpeedPlayer=1;

    protected SpriteRenderer spriteRenderer;
    protected bool flip=false;
    protected float time;

    protected virtual void Awake()
    {
        if (!PlayerPrefs.HasKey(name))
        {
            if (name == "Gun") PlayerPrefs.SetInt(name, 1);
            else PlayerPrefs.SetInt(name, 0);
        }
        if (!PlayerPrefs.HasKey("selectedGun")) PlayerPrefs.SetString("selectedGun", "Gun");
        if (PlayerPrefs.GetString("selectedGun") != name) gameObject.SetActive(false);
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
        if (Time.timeScale == 0) AudioPause();
        else AudioStart();
        if (gameObject.activeSelf) player.KSpeedFromWeapon = KSpeedPlayer;
        if (Time.timeScale != 0)
        {
            MakeRotation();
            Shot();
        }
    }

    //protected virtual void Flip()
    //{
    //    if (transform.rotation.y==0 && flip== true)
    //    {
    //        transform.position = new Vector3(-transform.position.x, transform.position.y);
    //        flip = true;
    //    }
    //    if (transform.rotation.x == -180 && flip == false)
    //    {
    //        transform.position = new Vector3(-transform.position.x, transform.position.y);
    //        flip = false;
    //    }
    //}

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
                PlaySound(0,0.2f);
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
