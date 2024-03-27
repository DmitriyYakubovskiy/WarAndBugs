using TMPro;
using UnityEngine;

public class ShopItemWithLevel : ShopItem
{
    [SerializeField] protected TextMeshProUGUI buttonBuyText;
    [SerializeField] protected TextMeshProUGUI levelCountText;
    [SerializeField] protected int maxLevel = 10;

    public int Level { get; set; } = 0;

    protected override void Awake()
    {
        Cost=starterCost;
        RecalculateTheCost();
        ChangeLevel();
    }

    public int GetMaxLevel()
    {
        return maxLevel;
    }

    public override void Buy()
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        if (Cost <= money && Level < maxLevel)
        {
            Level += 1;
            PlayerPrefs.SetInt("money", money - Cost);
            PlayerPrefs.SetInt(NameItem, Level);
            RecalculateTheCost();
            ChangeLevel();
            buySustem.UpdateOtherButtons();
        }
    }

    public void RecalculateTheCost()
    {
        if (PlayerPrefs.HasKey(NameItem))
        {
            Cost = starterCost;
            Level = PlayerPrefs.GetInt(NameItem);
            for (int i = 0; i < Level; i++)
            {
                Cost = (int)(Cost * 1.8f);
            }
        }
        else
        {
            PlayerPrefs.SetInt(NameItem, 0);
        }
    }

    public void ChangeLevel()
    {
        moneyText.text = Cost.ToString();
        if (Level == 0) SetText("Buy");
        else if (Level == maxLevel) SetText("Max Level");
        else SetText("Upgrade");
        levelCountText.text = $"{Level}/{maxLevel}";
    }

    private void SetText(string text)
    {
        if (LanguageManager.curretLanguage == "Russia") buttonBuyText.text = LanguageManager.EnglishToRussiaTranslator[text];
        else buttonBuyText.text = text;
    }
}
