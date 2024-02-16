using UnityEngine;
using System.Threading.Tasks;

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
    private float timeLeft1 = 0f;
    private float timeLeft2 = 0f;

    private void Update()
    {
        timeLeft1 += Time.deltaTime;
        timeLeft2 += Time.deltaTime;
        if (Mathf.FloorToInt(timeLeft1 / 30)==1)
        {
            startTime *= 0.96f;
            SpawnEntity(aiEntity[0]);
            timeLeft1 = 0;
        }
        if (Mathf.FloorToInt(timeLeft2 / 60) == 1)
        {
            SpawnEntity(aiEntity[1]);
            timeLeft2 = 0;
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
        if (value > 96 && aiEntity.Length >= 3 && player.Level > 3) gameObject = aiEntity[3];
        else if (value > 85 && aiEntity.Length >= 4 && player.Level > 4) gameObject = aiEntity[4];
        else if (value > 81 && aiEntity.Length >= 5 && player.Level > 6) gameObject = aiEntity[5];
        else gameObject = aiEntity[2];
        SpawnEntity(gameObject);
    }
    
    private bool DoteInScreen(Vector2 vector)
    {
        Vector2 playerPosition = player == null ? new Vector2(0, 0) : player.transform.position;
        Rect rect = new Rect(playerPosition.x-12, playerPosition.y-11,24,22);
        if (rect.Contains(vector)) return true;
        return false;
    }

    private void SpawnEntity(GameObject gameObject)
    {
        Vector2 playerPosition = player==null? new Vector2(0,0): player.transform.position;
        int positionX = Random.Range((int)playerPosition.x - 16, (int)playerPosition.x + 16);
        int positionY = Random.Range((int)playerPosition.y - 14, (int)playerPosition.y + 14);

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
