using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject player;
    int money;
    int index = 0;

    private void View()
    {
        if (player.IsDestroyed()) return;
        if (index < money - 1)
        {
            if(!this.IsDestroyed()) Invoke("View", 0.1f);
        }
        if (index == money)
        {
            moneyText.text = money.ToString();
        }
        else
        {
            index++;
            moneyText.text = index.ToString();
        }
    }

    public void UpdateFromMemory()
    {
        if (PlayerPrefs.HasKey("money")) moneyText.text = PlayerPrefs.GetInt("money").ToString();
        else moneyText.text=0.ToString();   
    }

    public void UpdateMoney(int money)
    {
        this.money = money;
        View();
    }
}