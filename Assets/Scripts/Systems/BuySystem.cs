using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuySystem : MonoBehaviour
{
    [SerializeField] private int[] cost;
    [SerializeField] private Button[] buttonsBuy;
    [SerializeField] private Button[] buttonsSelect;
    [SerializeField] private TextMeshProUGUI[] moneyTexts;
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Image[] panels;

    private Color unlockColor = new Color(152, 152, 152, 255);
    private Color lockColor = new Color(51, 51, 51, 255);

    public void Buy(int index)
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        if (cost[index] <= money)
        {
            PlayerPrefs.SetInt("money", money - cost[index]);
            PlayerPrefs.SetInt("gun_" + index, 1);
            buttonsBuy[index].interactable = false;
            buttonsSelect[index].interactable = true;
            UpdateButtonsAndMoney();
        }
    } 

    public void Select(int index)
    {
        if (PlayerPrefs.HasKey("selectedGun")) buttonsSelect[PlayerPrefs.GetInt("selectedGun")].interactable = true;
        buttonsSelect[index].interactable = false;
        PlayerPrefs.SetInt("selectedGun", index);
    }

    public void OnEnable()
    {
        UpdateButtonsAndMoney();
    }

    public void UpdateButtonsAndMoney()
    {
        if (!PlayerPrefs.HasKey("money")) PlayerPrefs.SetInt("money", 0);
        int money = PlayerPrefs.GetInt("money");
        moneyText.text = money.ToString();
        if (!PlayerPrefs.HasKey("selectedGun")) Select(0);
        PlayerPrefs.SetInt("gun_0", 1);
        for (int i = 0; i < buttonsBuy.Length; i++)
        {
            moneyTexts[i].text = cost[i].ToString();
            if (!PlayerPrefs.HasKey("gun_" + i)) PlayerPrefs.SetInt("gun_" + i, 0);
            if (PlayerPrefs.GetInt("gun_" + i) == 1)
            {
                buttonsSelect[i].interactable = true;
                buttonsBuy[i].interactable = false;
                if (PlayerPrefs.GetInt("selectedGun") == i) buttonsSelect[i].interactable = false;
            }
            else
            {
                //panels[i].color = Color.RGBA(1, 1, 1, 1);
                buttonsSelect[i].interactable = false;
                buttonsBuy[i].interactable = true;
                if (cost[i] > money) buttonsBuy[i].interactable = false;
            }
        }
    }

    public void Add100()
    {
        int money = PlayerPrefs.GetInt("money") + 100;
        PlayerPrefs.SetInt("money", money);
        UpdateButtonsAndMoney();
    }
}
