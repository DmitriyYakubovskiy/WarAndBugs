using TMPro;
using UnityEngine;

public class Player : Entity
{
    [SerializeField] private ResultPanel resultPanel;
    [SerializeField] private GameObject improvementSystem;
    [SerializeField] private GameObject diePanel;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private ExpBar expBar;
    private int level=1;
    private float exp;

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
            levelText.text ="LVL: "+level.ToString();
            improvementSystem.SetActive(true);
            Entities = (int)Mathf.Round((float)(Entities * 1.1));
        }
    }

    public int Entities { get; set; } = 7;
    public int Kills { get; set; } = 0;
    public float KDamage { get; set; } = 1;
    public float KReload { get; set; } = 1;
    public float KSpeed { get; set; } = 1;
    public float KSpeedFromWeapon { get; set; } = 1;

    private void Start()
    {
        hp.MaxHealth=Lives;
    }

    private void Update()
    {
        RechargeTimeDead();
        moveVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Move(false);
        hp.ShowHealth(this);
    }

    private void FixedUpdate()
    {
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
        if (moveVector.x < 0 && b == false) SetFlip(true);
        else if (moveVector.x > 0 && b == false) SetFlip(false);
        if (moveVector.x < 0 && b == true) SetAngles(true);
        else if (moveVector.x > 0 && b == true) SetAngles(false);

        moveVector = moveVector.normalized;
        moveVector = moveVector * (speed * KSpeed * KSpeedFromWeapon);
        rigidbody.velocity = moveVector;
    }


    private void OnDestroy()
    {
        resultPanel.levelText.text = "Level: " + Level;
        resultPanel.killsText.text = "Kills: " + Kills;
        if(diePanel != null) diePanel.SetActive(true);
    }
}
