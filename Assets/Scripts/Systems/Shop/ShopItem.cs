using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI moneyText;
    [SerializeField] protected GameObject buttonSelect;
    [SerializeField] protected GameObject buttonBuy;
    [SerializeField] protected BuySystem buySustem;
    [SerializeField] protected string selectedItemName = "selectedGun";
    [SerializeField] protected string nameItem;
    [SerializeField] protected int starterCost;

    public string SelectedItemName => selectedItemName;
    public string NameItem { get => nameItem; set { nameItem = value; } }
    public int Cost { get; set; }

    protected virtual void Awake()
    {
        Cost=starterCost;
        moneyText.text=Cost.ToString();
        if (NameItem == "Gun") if (!PlayerPrefs.HasKey(NameItem)) Buy();
        else if (!PlayerPrefs.HasKey(NameItem)) PlayerPrefs.SetInt(NameItem, 0);
        if (!PlayerPrefs.HasKey(selectedItemName)) if (NameItem == "Gun") Select();
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
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        if (Cost <= money)
        {
            PlayerPrefs.SetInt("money", money - Cost);
            PlayerPrefs.SetInt(NameItem, 1);
            buySustem.UpdateGunButtons();
        }
    }

    public virtual void Select()
    {
        PlayerPrefs.SetString(selectedItemName, NameItem);
        buySustem.UpdateGunButtons();
    }
}
