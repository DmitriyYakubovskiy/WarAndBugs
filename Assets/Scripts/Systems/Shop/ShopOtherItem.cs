using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopOtherItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI buttonBuyText;
    [SerializeField] private GameObject buttonBuy;
    [SerializeField] private BuySystem buySustem;
    [SerializeField] private string namesItem;
    [SerializeField] private int starterCost;

    private int cost;

    public static int MAX_LEVEL = 10;

    public string NamesItem { get=>namesItem; set=>namesItem=value; }
    public int Cost { get => cost; set => cost = value; }
    public int Level { get; set; } = 0;

    private void Awake()
    {
        cost=starterCost;
        RecalculateTheCost();
        ChangeLevel();
    }

    public void RecalculateTheCost()
    {
        if (PlayerPrefs.HasKey(namesItem))
        {
            cost = starterCost;
            Level = PlayerPrefs.GetInt(namesItem);
            for (int i = 0; i < Level; i++)
            {
                cost = (int)(cost * 1.8f);
            }
        }
        else
        {
            PlayerPrefs.SetInt(namesItem, 0);
        }
    }

    public void ChangeLevel()
    {
        moneyText.text = cost.ToString();
        if (Level == 0) SetText("Buy");
        else if (Level == MAX_LEVEL) SetText("Max Level");
        else SetText("Upgrade");
    }

    public void SetText(string text)
    {
        if (LanguageManager.curretLanguage == "Russia") buttonBuyText.text = LanguageManager.EnglishToRussiaTranslator[text];
        else buttonBuyText.text = text;
    }

    public void Buy()
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        if (Cost <= money && Level < MAX_LEVEL)
        {
            Level += 1;
            PlayerPrefs.SetInt("money", money - Cost);
            PlayerPrefs.SetInt(namesItem, Level);
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