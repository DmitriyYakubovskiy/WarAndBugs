using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    int money;
    int i = 0;

    private void View()
    {
        if (i < money - 1)
        {
            Invoke("View", 0.1f);
        }
        if (i == money)
        {
            moneyText.text = money.ToString();
        }
        else
        {
            i++;
            moneyText.text = i.ToString();

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