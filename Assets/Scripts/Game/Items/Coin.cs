using UnityEngine;

public class Coin : Item
{
    [SerializeField] private int MaxMoney = 25;
    [SerializeField] private int MinMoney = 5;
    private int Money;

    private void Start()
    {
        int value = Random.Range(MinMoney, MaxMoney) + 1;
        Money = (value/5) * 5;
        Debug.Log($"Coin value: {Money}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlaySound(0, volume, isDestroyed: true);
            collision.gameObject.GetComponent<Player>().Money += Money;
            Destroy(gameObject);
        }
    }
}
