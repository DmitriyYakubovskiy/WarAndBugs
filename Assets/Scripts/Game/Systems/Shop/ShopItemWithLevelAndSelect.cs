using UnityEngine;

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

    public override void Select()
    {
        SaveSystem.Data.SelectedItemNames[selectedItemName] = NameItem;
        buySystem.UpdateOtherButtons();
    }
}

