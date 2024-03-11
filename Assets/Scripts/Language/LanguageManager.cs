using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{
    public static Dictionary<string, string> EnglishToRussiaTranslator=new Dictionary<string, string>()
    {
        {"Back", "Назад"},
        {"Play", "Играть"},
        {"Collection", "Коллекция"},
        {"Store", "Магазин"},
        {"Delete", "Удалить"},
        {"Settings", "Настройки"},
        {"Exit", "Выйти"},
        {"Select", "Выбрать" },
        {"Buy", "Купить" },
        {"Guns", "Оружие" },
        {"Other", "Другое" },
        {"Upgrade", "Улучшить" },
        {"Time: ", "Время: " },
        {"Level: ", "Уровень: " },
        {"Kills: ", "Убийства: " },
        {"Results", "Итоги" },
        {"Menu", "Меню" },
        {"Restart", "Переиграть" },
        {"LVL: ", "УРВ: " },
        {"You Died", "Вы умерли" },
        {"Continue", "Продолжить" },
        {"To menu", "Меню" },
        {"Level Up!", "Новый уровень!" },
        {"HeatPoint\n+ 10%", "Здоровье\r\n+ 10%" },
        {"Reload\n- 5%", "Перезарядка\r\n- 5%" },
        {"Damage\n+ 5%", "Урон\r\n+ 5%" },
        {"Move speed\n+ 3%", "Бег\r\n+ 3%" },
        {"Audio", "Аудио" },
        {"Language", "Язык"},
        {"Max Level", "Макс. Урв."},
        {"LVL: 1", "УРВ: 1"}
    };

    private TextMeshProUGUI[] texts;
    [SerializeField] static public string curretLanguage = "English";
    [SerializeField] private TMP_Dropdown dropdown;

    private void Awake()
    {
        texts= GameObject.FindObjectsOfType<TextMeshProUGUI>(true);
        string s = PlayerPrefs.GetString("Language", curretLanguage);
        curretLanguage = s;
        if (curretLanguage == "English") dropdown.value = 0;
        if (curretLanguage == "Russia") dropdown.value = 1;
        UpdateLanguage();
    }

    public void ChangeLanguage(int val)
    {
        if (val == 0)
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (EnglishToRussiaTranslator.ContainsValue(texts[i].text)) texts[i].text = EnglishToRussiaTranslator.FirstOrDefault(x => x.Value == texts[i].text).Key; ;
            }
            curretLanguage = "English";
        }
        if (val == 1)
        {
            for (int i = 0; i < texts.Length; i++)
            { 
                if (EnglishToRussiaTranslator.ContainsKey(texts[i].text)) texts[i].text = EnglishToRussiaTranslator[texts[i].text];
            }
            curretLanguage = "Russia";
        }
    }

    public void UpdateLanguage()
    {
        if (curretLanguage == "Russia")
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (EnglishToRussiaTranslator.ContainsKey(texts[i].text)) texts[i].text = EnglishToRussiaTranslator[texts[i].text];
            }
            curretLanguage = "Russia";
        }
        if (curretLanguage == "English")
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (EnglishToRussiaTranslator.ContainsValue(texts[i].text)) texts[i].text = EnglishToRussiaTranslator.FirstOrDefault(x => x.Value == texts[i].text).Key; ;
            }
            curretLanguage = "English";
        }
    }

    public static string TranslateText(string text)
    {
        string s = text;
        if (curretLanguage == "Russia") s =EnglishToRussiaTranslator[text];
        else s = text;
        return s;
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetString("Language", curretLanguage);
    }
}
