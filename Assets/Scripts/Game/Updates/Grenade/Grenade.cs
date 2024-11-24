using UnityEngine;

public class Grenade : Sound
{
    [SerializeField] private GameObject grenadeShell;
    [SerializeField] private GameObject[] grenadeImages;
    [SerializeField] private Transform transformPoint;
    [SerializeField] private float damage;
    [SerializeField] private float radius;

    private float startTime = 0.5f;
    private float time = 0;

    private int maxCount = 0;
    private int count = 0;

    public int MaxCount => maxCount;
    public int Count
    {
        get=>count;
        set
        {
            count = value;
            if (count == 2)
            {
                for (int i = 0; i < grenadeImages.Length; i++) grenadeImages[i].SetActive(true);
                gameObject.SetActive(true);
            }
            if (count == 1)
            {
                grenadeImages[0].SetActive(true);
                grenadeImages[1].SetActive(false);
                gameObject.SetActive(true);
            }
            if (count == 0)
            {
                for (int i = 0; i < grenadeImages.Length; i++) grenadeImages[i].SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        if (SaveSystem.Data.ShopItemNames["Grenade"] == 0)
        {
            GrenadeSetActive(false);
            maxCount = 0;
            Count = maxCount;
        }
        else
        {
            maxCount = 1;
            Count = maxCount;
        }
        if (SaveSystem.Data.ShopItemNames["Grenade"] !=0)
        {
            int level = SaveSystem.Data.ShopItemNames["Grenade"];
            for (int i = 0; i < level; i++)
            {
                damage *= 1.5f;
                radius *= 1.2f;
            }
        }
        if (SaveSystem.Data.ShopItemNames["GrenadeBag"] !=0)
        {
            if (SaveSystem.Data.ShopItemNames["GrenadeBag"] ==1)
            { 
                maxCount = 2;
                Count = maxCount;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButton(1)) Attack();
        if (Time.timeScale != 0) MakeRotation();
        RechargeTime();
    }

    public void GrenadeSetActive(bool active)
    {
        if (active && count < maxCount) Count++;
    }

    protected virtual void MakeRotation()
    {
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transformPoint.position;
        float rotationZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float rotationY = transformPoint.eulerAngles.y;
        if (Mathf.Abs(rotationZ) >= 90 && rotationY == 0)
        {
            rotationY = 180;
            transformPoint.eulerAngles = new Vector3(0, rotationY, 180 - rotationZ);
        }
        if (Mathf.Abs(rotationZ) < 90 && rotationY == 180)
        {
            rotationY = 0;
            transformPoint.eulerAngles = new Vector3(0, rotationY, -rotationZ);
        }
        if (rotationY == 180)
        {
            transformPoint.eulerAngles = new Vector3(0, rotationY, 180 - rotationZ);
        }
        if (rotationY == 0)
        {
            transformPoint.eulerAngles = new Vector3(0, rotationY, rotationZ);
        }
    }

    private void RechargeTime()
    {
        if (time >= 0) time-=Time.deltaTime;
    }

    private void Attack()
    {
        if (time > 0) return;

        PlaySound(0, volume, isDestroyed: true);

        var objectGrenade = Instantiate(grenadeShell, transformPoint.position, transform.rotation);
        objectGrenade.SetActive(false);
        objectGrenade.GetComponent<GrenadeShell>().damage = damage;
        objectGrenade.GetComponent<GrenadeShell>().radius = radius;
        objectGrenade.SetActive(true);

        Count--;
        time=startTime;
        if (count <= 0) GrenadeSetActive(false);
    }
}
