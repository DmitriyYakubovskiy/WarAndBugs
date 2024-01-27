using UnityEngine;

public class BuySystem : MonoBehaviour
{
    [SerializeField] private int[] cost;

    public void Buy(int index)
    {
        int money = PlayerPrefs.GetInt("money");

        if (cost[index] < money)
        {
            PlayerPrefs.SetInt("money", money - cost[index]);
            PlayerPrefs.SetInt("gun_" + index, 1);
        }
    }

    
}
