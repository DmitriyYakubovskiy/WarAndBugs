using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI moneyText;
    [SerializeField] protected GameObject buttonSelect;
    [SerializeField] protected GameObject buttonBuy;
    [SerializeField] protected BuySystem buySystem;
    [SerializeField] protected string selectedItemName = "selectedGun";
    [SerializeField] protected string nameItem;
    [SerializeField] protected int starterCost;

    public string SelectedItemName => selectedItemName;
    public string NameItem { get => nameItem; set { nameItem = value; } }
    public int Cost { get; set; } = 0;

    protected virtual void Awake()
    {
        Cost = starterCost;
        moneyText.text = Cost.ToString();
    }

    public virtual void SetButtonSelectInteractable(bool b)
    {
        buttonSelect.GetComponent<Button>().interactable = b;
    }

    public virtual void SetButtonBuyInteractable(bool b)
    {
        buttonBuy.GetComponent<Button>().interactable = b;
    }

    public virtual void Buy()
    {
        if (SaveSystem.Data.ShopItemNames[NameItem] != 0) return;
        int money = SaveSystem.Data.money;
        if (Cost <= money)
        {
            SaveSystem.Data.money -= Cost;
            SaveSystem.Data.ShopItemNames[NameItem] = 1;
            buySystem.UpdateGunButtons();
        }
    }

    public virtual void Select()
    {
        SaveSystem.Data.SelectedItemNames[SelectedItemName] = NameItem;
        buySystem.UpdateGunButtons();
    }
}
