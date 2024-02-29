using TMPro;
using UnityEngine;
using System.Linq;

public class BuySystem : MonoBehaviour
{
    [SerializeField] private ShopItem[] gunItems;
    [SerializeField] private ShopOtherItem[] otherItems;
    [SerializeField] private TextMeshProUGUI moneyText;

    public void SetButtonSelectInteractable(int id,bool b)
    {
        gunItems[id].SetButtonSelectInteractable(b);
    }

    public void SetButtonBuy(string name)
    {
        gunItems.FirstOrDefault(x => x.NamesItem == name)?.Buy();
    }

    public void SetButtonSelect(string name)
    {
        gunItems.FirstOrDefault(x => x.NamesItem == name)?.Select();
    }

    public void SetButtonSelectInteractable(string name, bool b)
    {
        gunItems.FirstOrDefault(x => x.NamesItem == name)?.SetButtonSelectInteractable(b);
    }

    public void SetButtonBuyInteractable(int id, bool b)
    {
        gunItems[id].SetButtonBuyInteractable(b);
    }

    public void UpdateButtonsAndMoney()
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        moneyText.text = money.ToString();
        if (!PlayerPrefs.HasKey("Gun")) SetButtonBuy("Gun");
        if (!PlayerPrefs.HasKey("selectedGun")) SetButtonSelect("Gun");
        for (int i = 0; i < gunItems.Length; i++)
        {
            gunItems[i].SetButtonBuyInteractable(false);
            if (PlayerPrefs.GetInt(gunItems[i].NamesItem) == 1)
            {
                gunItems[i].SetButtonSelectInteractable(true);
                gunItems[i].SetButtonBuyInteractable(false);
                if (PlayerPrefs.GetString("selectedGun") == gunItems[i].NamesItem) gunItems[i].SetButtonSelectInteractable(false);
            }
            else
            {
                gunItems[i].SetButtonSelectInteractable(false);
                gunItems[i].SetButtonBuyInteractable(true);
                if (gunItems[i].Cost > money) gunItems[i].SetButtonBuyInteractable(false);
            }
        }
        for(int i= 0; i<otherItems.Length; i++)
        {
            otherItems[i].RecalculateTheCost();
            otherItems[i].ChangeLevel();
            if (otherItems[i].Cost > money && otherItems[i].Level < ShopOtherItem.MAX_LEVEL) otherItems[i].SetButtonBuyInteractable(false);
            else otherItems[i].SetButtonBuyInteractable(true);
        }
    }

    public void Add100()
    {
        int money = PlayerPrefs.GetInt("money") + 100;
        PlayerPrefs.SetInt("money", money);
        UpdateButtonsAndMoney();
    }

    public void OnEnable()
    {
        UpdateButtonsAndMoney();
    }
}
