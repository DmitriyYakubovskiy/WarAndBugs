using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject buttonSelect;
    [SerializeField] private GameObject buttonBuy;
    [SerializeField] private BuySystem buySustem;
    [SerializeField] private string namesItem;
    [SerializeField] private int cost;

    public string NamesItem { get => namesItem; set => namesItem = value; }
    public int Cost { get => cost; set => cost = value;}

    private void Awake()
    {
        moneyText.text=Cost.ToString();
        if (!PlayerPrefs.HasKey(namesItem)) if (namesItem == "Gun") Buy();
        if (!PlayerPrefs.HasKey("selectedGun")) if (namesItem == "Gun") Select();
    }

    public void Buy()
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        if (Cost <= money)
        {
            PlayerPrefs.SetInt("money", money - Cost);
            PlayerPrefs.SetInt(namesItem, 1);
            buttonBuy.GetComponent<Button>().interactable = false;
            buttonSelect.GetComponent<Button>().interactable = true;
            buySustem.UpdateButtonsAndMoney();
        }
    }

    public void Select()
    {
        if (PlayerPrefs.HasKey("selectedGun")) buySustem.SetButtonSelectInteractable(PlayerPrefs.GetString("selectedGun"),true);
        SetButtonSelectInteractable(false);
        PlayerPrefs.SetString("selectedGun", namesItem);
    }

    public void SetButtonSelectInteractable(bool b)
    {
        buttonSelect.GetComponent<Button>().interactable = b;
    }

    public void SetButtonBuyInteractable(bool b)
    {
        buttonBuy.GetComponent<Button>().interactable = b;
    }
}
