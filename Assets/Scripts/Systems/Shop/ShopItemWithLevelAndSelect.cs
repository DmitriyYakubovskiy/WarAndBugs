using UnityEngine;
using UnityEngine.UI;

public class ShopItemWithLevelAndSelect : ShopItemWithLevel
{
    protected override void Awake()
    {
        Cost = starterCost;
        RecalculateTheCost();
        ChangeLevel();
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

    public override void Select()
    {
        PlayerPrefs.SetString(selectedItemName, NameItem);
        buySustem.UpdateOtherButtons();
    }
}

