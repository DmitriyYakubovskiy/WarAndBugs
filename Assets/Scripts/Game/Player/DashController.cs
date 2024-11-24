using Assets.Scripts.MenuAndUI;
using UnityEngine;

public class DashController : Sound 
{
    [SerializeField] private GameObject DashImageObject;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private float dashPower = 2;
    [SerializeField] private float startTime = 10f;
    private Player player;
    private SpriteRenderer DashImage;
    private float time = 0;

    private float startTimeImage = 0.2f;
    private float timeImage = 0;

    private void Start()
    {
        if (SaveSystem.Data.ShopItemNames["Dash"] != 0) this.enabled = true;
        else this.enabled = false;

        time =startTime;
        progressBar.MaxPoints = startTime;
        DashImage = DashImageObject.GetComponent<SpriteRenderer>();
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && time>=startTime) Dash();
        if (time < startTime)
        {
            time += Time.deltaTime;
            progressBar.ChangePoints(time);
        }

        if (timeImage>0 && DashImageObject.activeSelf) timeImage-=Time.deltaTime;
        else DashImageObject.SetActive(false);
    }

    private void Dash()
    {
        if (player.MoveVector.x == 0 && player.MoveVector.y == 0) return;
        PlaySound(0, volume);
        timeImage = startTimeImage;
        DashImageObject.SetActive(true);
        DashImage.flipX = player.MoveVector.x>0?false:true;
        time = 0;
        transform.Translate(player.MoveVector * dashPower);
    }


}
