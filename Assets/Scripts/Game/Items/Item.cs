using UnityEngine;

public class Item : Sound
{
    protected Player player;
    protected float liveTime = 0;

    public const float MaxLifeTime = 30;
    public const float MaxDistanceToPlayer = 14;

    protected virtual void Start()
    {
        SearchPlayer();
    }
    protected virtual void SearchPlayer()
    {
        if (player == null) player = GameObject.Find("Player").GetComponent<Player>();
    }

    protected virtual void Update()
    {
        if (player == null) return;
        liveTime += Time.deltaTime;
        if (liveTime > MaxLifeTime && Vector3.Distance(player.transform.position, gameObject.transform.position) > MaxDistanceToPlayer)
        {
            gameObject.transform.position = SpawnSystem.GetNewPosition(player);
            liveTime = 0;
        }
    }
}
