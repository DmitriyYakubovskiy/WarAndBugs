using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] aiEntity;
    [SerializeField] private Player player;
    [SerializeField] private float startTime;
    [SerializeField] private float rangeTop;
    [SerializeField] private float rangeRight;    
    [SerializeField] private float rangeBottom;
    [SerializeField] private float rangeLeft;

    private float time = 0;
    private int range = 25;
    private float timeLeft = 0f;

    private void Update()
    {
        timeLeft += Time.deltaTime;
        if (Mathf.FloorToInt(timeLeft / 30)==1)
        {
            startTime *= 0.96f;
            SpawnEntity(aiEntity[0]);
            timeLeft = 0;
        }
        if (RechargeTimeSpawn())
        {
            ChooseRandomEntity();
            time = startTime;
        }
    }

    private bool RechargeTimeSpawn()
    {
        time -= Time.deltaTime;
        if (time > 0) return false;
        return true;
    }

    private void ChooseRandomEntity()
    {
        GameObject gameObject;
        int value = UnityEngine.Random.Range(0, 101);
        if (value > 99 && aiEntity.Length >= 3 && player.Level > 5) gameObject = aiEntity[2];
        else if (value > 96 && aiEntity.Length >= 4 && player.Level > 8) gameObject = aiEntity[3];
        else if (value > 94 && aiEntity.Length >= 5 && player.Level > 10) gameObject = aiEntity[4];
        else gameObject = aiEntity[1];
        SpawnEntity(gameObject);
    }

    private bool DoteInScreen(Vector2 vector)
    {
        Vector2 playerPosition = player == null ? new Vector2(0, 0) : player.transform.position;
        Rect rect = new Rect(playerPosition.x-10, playerPosition.y-9,20,18);
        if (rect.Contains(vector)) return true;
        return false;
    }

    private void SpawnEntity(GameObject gameObject)
    {
        Vector2 playerPosition = player==null? new Vector2(0,0): player.transform.position;
        int positionX = Random.Range((int)playerPosition.x - 12, (int)playerPosition.x + 12);
        int positionY = Random.Range((int)playerPosition.y - 11, (int)playerPosition.y + 11);

        while (DoteInScreen(new Vector2(positionX, positionY)))
        {
            if (positionX >= 0 && positionY >= 0)
            {
                positionX += 2;
                positionY += 2;
            }
            if (positionX >= 0 && positionY <= 0)
            {
                positionX += 2;
                positionY -= 2;
            }
            if (positionX <= 0 && positionY <= 0)
            {
                positionX -= 2;
                positionY -= 2;
            }
            if (positionX <= 0 && positionY >= 0)
            {
                positionX -= 2;
                positionY += 2;
            }
        }
        if (positionX > rangeRight) positionX = positionX - range * 2;
        if (positionX < rangeLeft) positionX = positionX + range*2;
        if (positionY > rangeTop) positionY = positionY - range * 2;
        if (positionY < rangeBottom) positionY = positionY + range * 2;

        GameObject entity = Instantiate(gameObject);
        entity.transform.position = new Vector3(positionX, positionY, 0);
    }
}
