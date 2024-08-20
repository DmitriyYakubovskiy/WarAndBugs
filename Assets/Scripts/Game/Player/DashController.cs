using UnityEngine;

public class DashController : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Dash();
    }

    private void Dash()
    {
        transform.Translate(player.MoveVector * 2);
    }
}
