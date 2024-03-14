using UnityEngine;
using UnityEngine.UIElements;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] aiEntity;
    [SerializeField] private GameObject[] events;
    [SerializeField] private float startTime;
    [SerializeField] private float rangeTop;
    [SerializeField] private float rangeRight;    
    [SerializeField] private float rangeBottom;
    [SerializeField] private float rangeLeft;

    private float time = 0;
    private float timeHealth = 0f;
    private float timeLeftHealthMax = 0f;
    private float timeLeftGrenade = 0f;
    private float timeEvents=0f;

    private static Player player;
    private static int range = 25;
    private static float RangeTop { get; set; }
    private static float RangeRight { get; set; }
    private static float RangeBottom { get; set; }
    private static float RangeLeft { get; set; }

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        RangeTop = rangeTop;
        RangeRight = rangeRight;
        RangeBottom = rangeBottom;
        RangeLeft = rangeLeft;
    }

    private void Update()
    {
        timeHealth += Time.deltaTime;
        timeLeftHealthMax += Time.deltaTime;
        timeLeftGrenade += Time.deltaTime;
        timeEvents += Time.deltaTime;   
        if (Mathf.FloorToInt(timeHealth / 61)==1)
        {
            startTime *= 0.91f;
            SpawnEntity(aiEntity[0]);
            timeHealth = 0;
        }
        if (Mathf.FloorToInt(timeLeftHealthMax / 125) == 1)
        {
            SpawnEntity(aiEntity[1]);
            timeLeftHealthMax = 0;
        }
        if (Mathf.FloorToInt(timeLeftGrenade / 106) == 1 && PlayerPrefs.GetInt("Grenade")!=0)
        {
            SpawnEntity(aiEntity[2]);
            timeLeftGrenade = 0;
        }
        if (Mathf.FloorToInt(timeEvents / 57) == 1)
        {
            SpawnEvent();
            timeEvents = 0;
        }
        if (RechargeTimeSpawn())
        {
            ChooseRandomEntity();
            time = startTime;
        }
    }

    private void SpawnEvent()
    {
        int index = UnityEngine.Random.Range(0, events.Length);
        GameObject entity = Instantiate(events[index]);
        if(index==0) entity.transform.position = new Vector3((RangeLeft+RangeRight)/2, RangeTop*2, 0);
        if (index == 1)
        {
            entity.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
            for (int i = 0; i < entity.transform.childCount; i++)
            {
                Debug.Log(i);
                entity.transform.GetChild(i).position = new Vector3(entity.transform.GetChild(i).position.x, entity.transform.GetChild(i).position.y,0);
                if (entity.transform.GetChild(i).position.x > RangeRight || entity.transform.GetChild(i).position.x < RangeLeft
                    || entity.transform.GetChild(i).position.y > RangeTop || entity.transform.GetChild(i).position.y < RangeBottom) Destroy(entity.transform.GetChild(i).gameObject);
            }
        }
        if (index == 2) entity.transform.position = GetNewPosition();
    }

    public static Vector2 GetNewPosition()
    {
        Vector2 playerPosition = player == null ? new Vector2(0, 0) : player.transform.position;
        int positionX = Random.Range((int)playerPosition.x - 18, (int)playerPosition.x + 18);
        int positionY = Random.Range((int)playerPosition.y - 18, (int)playerPosition.y + 18);

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
        if (positionX > RangeRight) positionX = positionX - range;
        if (positionX < RangeLeft) positionX = positionX + range;
        if (positionY > RangeTop) positionY = positionY - range;
        if (positionY < RangeBottom) positionY = positionY + range;


        return new Vector2(positionX, positionY);
    }

    private bool RechargeTimeSpawn()
    {
        time -= Time.deltaTime;
        if (time > 0) return false;
        return true;
    }

    private void ChooseRandomEntity()
    {
        if(player == null) player = GameObject.Find("Player")?.GetComponent<Player>();
        GameObject gameObject;
        int value = UnityEngine.Random.Range(0, 101);
        if (value > 96 && aiEntity.Length >= 3 && player.Level > 3) gameObject = aiEntity[4];
        else if (value > 85 && aiEntity.Length >= 4 && player.Level > 4) gameObject = aiEntity[5];
        else if (value > 81 && aiEntity.Length >= 5 && player.Level > 6) gameObject = aiEntity[6];
        else gameObject = aiEntity[3];
        SpawnEntity(gameObject);
    }
    
    private static bool DoteInScreen(Vector2 vector)
    {
        Vector2 playerPosition = player == null ? new Vector2(0, 0) : player.transform.position;
        Rect rect = new Rect(playerPosition.x-13, playerPosition.y-11,26,22);
        if (rect.Contains(vector)) return true;
        return false;
    }

    private static bool DoteInMap(Vector2 vector)
    {
        Rect rect = new Rect(-46, -78, 124, 92);
        if (rect.Contains(vector)) return true;
        return false;
    }

    private void SpawnEntity(GameObject gameObject)
    {
        Vector2 vector = GetNewPosition();
        GameObject entity = Instantiate(gameObject);
        entity.transform.position = new Vector3(vector.x, vector.y, 0);
    }
}
