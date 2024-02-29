using UnityEngine;

public class Grenade : Sound
{
    [SerializeField] private GameObject granageShell;
    [SerializeField] private Transform transformPoint;
    [SerializeField] private float damage;
    [SerializeField] private float radius;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("Grenade") == 0) gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("Grenade"))
        {
            int level = PlayerPrefs.GetInt("Grenade");
            for (int i = 0; i < level; i++)
            {
                damage *= 1.5f;
                radius *= 1.2f;
            }
        }
        else
        {
            PlayerPrefs.SetInt("Grenade",0);
        }
    }

    private void Update()
    {
        if (Time.timeScale != 0)
        {
            MakeRotation();
            Attack();
        }
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

    private void Attack()
    {
        if (Input.GetMouseButton(1))
        {
            PlaySound(0, volume, isDestroyed: true);
            var objectGrenage=Instantiate(granageShell, transformPoint.position, transform.rotation);
            objectGrenage.SetActive(false);
            objectGrenage.GetComponent<GrenadeShell>().damage = damage;
            objectGrenage.GetComponent<GrenadeShell>().radius = radius;
            objectGrenage.SetActive(true);
            gameObject.SetActive(false);  
        }
    }
}
