using UnityEngine;

public class Box : Entity
{
    [SerializeField] private GameObject obj;

    protected override void Start()
    {
        base.Start();
        hp.MaxHealth = Lives;
    }

    public override void TakeDamage(float damage, bool bonus = true)
    {
        if (Lives <= 0) return;
        Lives -= damage;
        if (Lives <= 0)
        {
            Lives = 0;
            isDead = true;
            gameObject.GetComponent<Collider2D>().enabled = false;
            SpawnObject();
            PlaySound(2, volume/2);
        }
        else
        {
            PlaySound(1, volume);
        }
    }

    private void Update()
    {
        RechargeTimeDead();
    }

    private void FixedUpdate()
    {
        if (isDead) State = States.Dead;
        else State = States.Idle;
    }

    private void SpawnObject()
    {
        var spawnObj = Instantiate(obj);
        spawnObj.transform.position = transform.position;
    }

    protected override void RechargeTimeDead()
    {
        if (!isDead) return;
        if (timeDeadAnimation > 0) timeDeadAnimation -= Time.deltaTime;
        else
        {
            Destroy(this.gameObject);
        }
    }
}
