using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopOtherItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI buttonBuyText;
    [SerializeField] private GameObject buttonBuy;
    [SerializeField] private BuySystem buySustem;
    [SerializeField] private string name;
    [SerializeField] private int starterCost;

    private int cost;

    public static int MAX_LEVEL = 10;

    public string Name { get=>name; set=>name=value; }
    public int Cost { get => cost; set => cost = value; }
    public int Level { get; set; } = 0;

    private void Awake()
    {
        Debug.Log(cost);
        cost=starterCost;
        Debug.Log(starterCost);
        RecalculateTheCost();
        ChangeLevel();
    }

    public void RecalculateTheCost()
    {
        if (PlayerPrefs.HasKey(name))
        {
            cost = starterCost;
            Level = PlayerPrefs.GetInt(name);
            for (int i = 0; i < Level; i++)
            {
                cost = (int)(cost * 1.8f);
            }
        }
        else
        {
            PlayerPrefs.SetInt(name, 0);
        }
    }

    public void ChangeLevel()
    {
        moneyText.text = cost.ToString();
        if (Level == 0) buttonBuyText.text = "Buy";
        else if (Level == MAX_LEVEL) buttonBuyText.text = "Max Level";
        else buttonBuyText.text = "Upgrade";
    }

    public void Buy()
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        if (Cost <= money && Level < MAX_LEVEL)
        {
            Level += 1;
            PlayerPrefs.SetInt("money", money - Cost);
            PlayerPrefs.SetInt(name, Level);
            RecalculateTheCost();
            ChangeLevel();
            buySustem.UpdateButtonsAndMoney();
        }
    }

    public void SetButtonBuyInteractable(bool b)
    {
        buttonBuy.GetComponent<Button>().interactable = b;
    }
}
