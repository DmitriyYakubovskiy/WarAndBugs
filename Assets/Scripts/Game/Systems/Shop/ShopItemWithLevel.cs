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
        int money = SaveSystem.Data.money;
        if (Cost <= money && Level < maxLevel)
        {
            Level += 1;
            SaveSystem.Data.money -= Cost;
            SaveSystem.Data.ShopItemNames[NameItem] = Level;
            RecalculateTheCost();
            ChangeLevel();
            buySystem.UpdateOtherButtons();
        }
    }

    public void RecalculateTheCost()
    {
        Cost = starterCost;
        Level = SaveSystem.Data.ShopItemNames[NameItem];
        for (int i = 0; i < Level; i++)
        {
            Cost = (int)(Cost * 1.8f);
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
        if (LanguageManager.currentLanguage == "Russia") buttonBuyText.text = LanguageManager.EnglishToRussiaTranslator[text];
        else buttonBuyText.text = text;
    }
}
