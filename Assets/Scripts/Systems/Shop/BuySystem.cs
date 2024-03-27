using TMPro;
using UnityEngine;

public class BuySystem : MonoBehaviour
{
    [SerializeField] private ShopItem[] gunItems;
    [SerializeField] private ShopItemWithLevel[] otherItems;
    [SerializeField] private ShopItemWithLevelAndSelect[] droneItems;
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        UpdateGunButtons();
        UpdateOtherButtons();
    }

    private int UpdateMoney()
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        moneyText.text = money.ToString();
        return money;
    }

    public void UpdateGunButtons()
    {
        int money = UpdateMoney();
        for (int i = 0; i < gunItems.Length; i++)
        {
            gunItems[i].SetButtonBuyInteractable(false);
            if (PlayerPrefs.GetInt(gunItems[i].NameItem) == 1)
            {
                gunItems[i].SetButtonSelectInteractable(true);
                gunItems[i].SetButtonBuyInteractable(false);
                if (PlayerPrefs.GetString(gunItems[i].SelectedItemName) == gunItems[i].NameItem) gunItems[i].SetButtonSelectInteractable(false);
            }
            else
            {
                gunItems[i].SetButtonSelectInteractable(false);
                gunItems[i].SetButtonBuyInteractable(true);
                if (gunItems[i].Cost > money) gunItems[i].SetButtonBuyInteractable(false);
            }
            if (gunItems[i].NameItem=="Gun") gunItems[i].SetButtonBuyInteractable(false);
        }
    }

    public void UpdateOtherButtons()
    {
        int money = UpdateMoney();
        for (int i = 0; i < droneItems.Length; i++)
        {
            droneItems[i].RecalculateTheCost();
            droneItems[i].ChangeLevel();
            droneItems[i].SetButtonBuyInteractable(false);
            if (PlayerPrefs.GetInt(droneItems[i].NameItem) >= 1)
            {
                droneItems[i].SetButtonSelectInteractable(true);
                if (droneItems[i].Cost > money || droneItems[i].Level >= droneItems[i].GetMaxLevel()) droneItems[i].SetButtonBuyInteractable(false);
                else droneItems[i].SetButtonBuyInteractable(true);
                if (PlayerPrefs.GetString(droneItems[i].SelectedItemName) == droneItems[i].NameItem) droneItems[i].SetButtonSelectInteractable(false);
            }
            else
            {
                droneItems[i].SetButtonSelectInteractable(false);
                droneItems[i].SetButtonBuyInteractable(true);
                if (droneItems[i].Cost > money) droneItems[i].SetButtonBuyInteractable(false);
            }
        }
        for (int i = 0; i < otherItems.Length; i++)
        {
            otherItems[i].RecalculateTheCost();
            otherItems[i].ChangeLevel();
            if (otherItems[i].Cost > money || otherItems[i].Level >= otherItems[i].GetMaxLevel()) otherItems[i].SetButtonBuyInteractable(false);
            else otherItems[i].SetButtonBuyInteractable(true);
        }
    }

    public void Add100()
    {
        int money = PlayerPrefs.GetInt("money") + 100;
        PlayerPrefs.SetInt("money", money);
        UpdateGunButtons();
        UpdateOtherButtons();
    }
}
