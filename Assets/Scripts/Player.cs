using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Entity
{
    [SerializeField] private ExpBar expBar;
    [SerializeField] private GameObject improvementSystem;
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
            improvementSystem.SetActive(true);
            Entities = (int)Mathf.Round((float)(Entities * 1.1));
        }
    }

    public int Entities { get; set; } = 7;
    public int Kills { get; set; } = 0;
    public float KDamage { get; set; } = 1;
    public float KReload { get; set; } = 1;

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
        if (moveVector.y == 0 && moveVector.x == 0) State=States.Idle;
        else State = States.Run;
        previousPosition = rigidbody.position;
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(0);
    }
}
