using System.Threading.Tasks;
using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] aiEntity;
    [SerializeField] private GameObject[] events;
    [SerializeField] private GameObject[] bosses;
    [SerializeField] private Player player;
    [SerializeField] private float startTime;
    [SerializeField] private float rangeTop;
    [SerializeField] private float rangeRight;    
    [SerializeField] private float rangeBottom;
    [SerializeField] private float rangeLeft;

    private float timeBoss = 0;
    private float time = 0;
    private float timeHealth = 0f;
    private float timeLeftHealthMax = 0f;
    private float timeLeftGrenade = 0f;
    private float timeEvents=0f;
    private float startTimeLeftHealth=60;
    private float startTimeLeftHealthMax = 125;
    private float startTimeLeftGrenade = 50;
    private float startTimeEvents = 70;

    private static int range = 25;
    private static int distanseToPlayer = 20;
    private static float RangeTop { get; set; }
    private static float RangeRight { get; set; }
    private static float RangeBottom { get; set; }
    private static float RangeLeft { get; set; }

    public bool IsBossStage { get; set; }
    public static int aliveBugs=0;
    public static int levelBugs=1;

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
        if (aliveBugs > 50 * levelBugs)
        {
            startTime *= 2;
            levelBugs += 1;
        }
        timeHealth += Time.deltaTime;
        timeLeftHealthMax += Time.deltaTime;
        timeLeftGrenade += Time.deltaTime;
        timeEvents += Time.deltaTime;
        timeBoss += Time.deltaTime;
        if (Mathf.FloorToInt(timeHealth / startTimeLeftHealth) == 1)
        {
            SpawnEntity(aiEntity[0]);
            startTimeLeftHealth = Random.Range(50, 70);
            timeHealth = 0;
            startTime *= 0.70f;
        }
        if (Mathf.FloorToInt(timeLeftHealthMax / startTimeLeftHealthMax) == 1)
        {
            SpawnEntity(aiEntity[1]);
            startTimeLeftHealthMax = Random.Range(100, 150);
            timeLeftHealthMax = 0;
        }
        if (Mathf.FloorToInt(timeLeftGrenade / startTimeLeftGrenade) == 1 && PlayerPrefs.GetInt("Grenade") != 0)
        {
            SpawnEntity(aiEntity[2]);
            startTimeLeftGrenade = Random.Range(45, 55);
            timeLeftGrenade = 0;
        }
        if (Mathf.FloorToInt(timeBoss / 600) >= 1 && player.Level>30 && IsBossStage == false)
        {
            IsBossStage = true;
            Invoke("SpawnBoss", 20f);
            timeBoss = 0;    
        }
        if (Mathf.FloorToInt(timeEvents / startTimeEvents) == 1 && IsBossStage==false)
        {
            SpawnEvent();
            startTimeEvents = Random.Range(60, 80);
            timeEvents = 0;
        }
        if (RechargeTimeSpawn() && IsBossStage == false)
        {
            ChooseRandomEntity();
            aliveBugs += 1;
            time=startTime;
        }
    }

    private bool RechargeTimeSpawn()
    {
        time-=Time.deltaTime;
        if(time<=0)
        {
            return true;
        }
        return false;
    }

    private void SpawnBoss()
    {
        SpawnEntity(bosses[0]);
    }

    private void SpawnEvent()
    {
        int index = 0;
        if(player.Level>5) index = UnityEngine.Random.Range(0, 2);
        if (player.Level > 7) index = UnityEngine.Random.Range(0, 6);
        if (player.Level > 10) index = UnityEngine.Random.Range(0, 7);

        GameObject entity = Instantiate(events[index]);

        if (index == 0) entity.transform.position = new Vector3((RangeLeft+rangeRight)/2+UnityEngine.Random.Range(-5, 5), RangeTop * 2, 0);
        if (index == 1)
        {
            entity.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0);
            for (int i = 0; i < entity.transform.childCount; i++)
            {
                entity.transform.GetChild(i).position = new Vector3(entity.transform.GetChild(i).position.x, entity.transform.GetChild(i).position.y,0);
                if (entity.transform.GetChild(i).position.x > RangeRight || entity.transform.GetChild(i).position.x < RangeLeft
                    || entity.transform.GetChild(i).position.y > RangeTop || entity.transform.GetChild(i).position.y < RangeBottom) Destroy(entity.transform.GetChild(i).gameObject);
            }
        }
        if (index == 2) entity.transform.position = new Vector3(player.transform.position.x - distanseToPlayer, player.transform.position.y, 0);
        if (index == 3) entity.transform.position = new Vector3(player.transform.position.x + distanseToPlayer, player.transform.position.y, 0);
        if (index == 4) entity.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + distanseToPlayer, 0);
        if (index == 5) entity.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - distanseToPlayer, 0);
        if (index == 6) entity.transform.position = GetNewPosition(player);
    }

    public static Vector2 GetNewPosition(Player player)
    {
        Vector2 playerPosition = player == null ? new Vector2(0, 0) : player.transform.position;
        int positionX = UnityEngine.Random.Range((int)playerPosition.x - distanseToPlayer, (int)playerPosition.x + distanseToPlayer);
        int positionY = UnityEngine.Random.Range((int)playerPosition.y - distanseToPlayer, (int)playerPosition.y + distanseToPlayer);

        while (DoteInScreen(new Vector2(positionX, positionY),player))
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

    private void ChooseRandomEntity()
    {
        if(player == null) player = GameObject.Find("Player")?.GetComponent<Player>();
        if (player == null) return;
        GameObject gameObject;
        int value = UnityEngine.Random.Range(0, 101);
        if (value > 96 && aiEntity.Length >= 3 && player.Level > 3) gameObject = aiEntity[4];
        else if (value > 85 && aiEntity.Length >= 4 && player.Level > 4) gameObject = aiEntity[5];
        else if (value > 81 && aiEntity.Length >= 5 && player.Level > 6) gameObject = aiEntity[6];
        else gameObject = aiEntity[3];
        SpawnEntity(gameObject);
    }
    
    private static bool DoteInScreen(Vector2 vector,Player player)
    {
        Vector2 playerPosition = player == null ? new Vector2(0, 0) : player.transform.position;
        Rect rect = new Rect(playerPosition.x-16, playerPosition.y-14,32,28);
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
        Vector2 vector = GetNewPosition(player);
        GameObject entity = Instantiate(gameObject);
        if (entity.tag=="Enemy") entity.GetComponent<Bug>().UpgradeCharacteristics(levelBugs);
        entity.transform.position = new Vector3(vector.x, vector.y, 0);
    }

    public void SpawnEntities(int counts)
    {
        for(int i = 0; i < counts; i++)
        {
            GameObject gameObject;
            int value = UnityEngine.Random.Range(0, 101);
            if (value > 90) gameObject = aiEntity[5];
            else if (value > 85) gameObject = aiEntity[6];
            else gameObject = aiEntity[3];
            SpawnEntity(gameObject);
        }
    }
}
