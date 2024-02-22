using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject buttonSelect;
    [SerializeField] private GameObject buttonBuy;
    [SerializeField] private BuySystem buySustem;
    [SerializeField] private string name;
    [SerializeField] private int cost;

    public string Name { get => name; set => name = value; }
    public int Cost { get => cost; set => cost = value;}

    private void Awake()
    {
        moneyText.text=Cost.ToString();
        if (!PlayerPrefs.HasKey(name))
        {
            if (name == "Gun") Buy();
            else PlayerPrefs.SetInt(name, 0);
        }
        if (!PlayerPrefs.HasKey("selectedGun"))
        {
            if (name == "Gun") Select();
            PlayerPrefs.SetString("selectedGun", "Gun");
        }
    }

    public void Buy()
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        if (Cost <= money)
        {
            PlayerPrefs.SetInt("money", money - Cost);
            PlayerPrefs.SetInt(name, 1);
            buttonBuy.GetComponent<Button>().interactable = false;
            buttonSelect.GetComponent<Button>().interactable = true;
            buySustem.UpdateButtonsAndMoney();
        }
    }

    public void Select()
    {
        if (PlayerPrefs.HasKey("selectedGun")) buySustem.SetButtonSelectInteractable(PlayerPrefs.GetString("selectedGun"),true);
        SetButtonSelectInteractable(false);
        PlayerPrefs.SetString("selectedGun", name);
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
