using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private ResultPanel resultPanel;
    [SerializeField] private GameObject improvementSystem;
    [SerializeField] private GameObject diePanel;
    [SerializeField] private GameObject grenade;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private ExpBar expBar;
    [SerializeField] private MoneyView moneyView;
    private int money = 0;
    private int level = 1;
    private float exp;
    private string currentLanguage;

    public float Exp
    {
        get => exp;
        set
        {
            exp = value;
            expBar.ShowExp();
        }
    }
    public int Level
    {
        get => level;
        set
        {
            level = value;
            levelText.text = LanguageManager.TranslateText("LVL: ") + level.ToString();
            improvementSystem.SetActive(true);
            Entities = (int)Mathf.Round((float)(Entities * 1.1));
        }
    }
    
    public int Money
    { 
        get=>money;
        set
        {
            money = value;
            moneyView.UpdateMoney(money); 
        }
    }

    public int Entities { get; set; } = 7;
    public int Kills { get; set; } = 0;
    public float KDamage { get; set; } = 1;
    public float KReload { get; set; } = 1;
    public float KSpeed { get; set; } = 1;
    public float KSpeedFromWeapon { get; set; } = 1;

    public void GrenadeSetActive(bool b)
    {
        grenade.SetActive(b);
    }

    public bool GrenadeGetActive()
    {
        return grenade.gameObject.activeSelf;
    }

    private void Start()
    {
        currentLanguage=LanguageManager.curretLanguage;
        hp.MaxHealth=Lives;
    }

    private void Update()
    {
        RechargeTimeDead();
        moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        hp.ShowHealth(this);
    }

    private void FixedUpdate()
    {
        Move(false);
        if (moveVector.y == 0 && moveVector.x == 0)
        {
            State = States.Idle;
        }
        else
        {
            SpeedCalculation();
            if (Animator.speed == 0) State = States.Idle;
            else State = States.Run;
        }
        previousPosition = rigidbody.position;
    }

    protected override void Move(bool b)
    {
        var dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (moveVector.x > dir.x && b == false) SetFlip(true);
        else if (moveVector.x < dir.x && b == false) SetFlip(false);
        if (moveVector.x > dir.x && b == true) SetAngles(true);
        else if (moveVector.x < dir.x && b == true) SetAngles(false);
        //if (moveVector.x < 0 && b == false) SetFlip(true);
        //else if (moveVector.x > 0 && b == false) SetFlip(false);
        //if (moveVector.x < 0 && b == true) SetAngles(true);
        //else if (moveVector.x > 0 && b == true) SetAngles(false);

        moveVector = moveVector.normalized;
        moveVector = moveVector * (speed * KSpeed * KSpeedFromWeapon);
        rigidbody.velocity = moveVector;
    }

    private void UpdateText()
    {
        if (LanguageManager.curretLanguage != currentLanguage)
        {
            levelText.text = LanguageManager.TranslateText("LVL: ") + level.ToString();
            currentLanguage = LanguageManager.curretLanguage;
        }
    }

    private void OnDestroy()
    {
        if (PlayerPrefs.HasKey("money"))
        {
            Money += PlayerPrefs.GetInt("money");
        }
        PlayerPrefs.SetInt("money", Money);
        resultPanel.levelText.text = LanguageManager.TranslateText("Level: ") + Level;
        resultPanel.killsText.text = LanguageManager.TranslateText("Kills: ") + Kills;
        if(!diePanel.gameObject.IsDestroyed()) diePanel?.SetActive(true);
    }
}
