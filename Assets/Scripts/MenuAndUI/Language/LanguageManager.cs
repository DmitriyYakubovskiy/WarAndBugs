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
        {"LVL: 1", "УРВ: 1"},
        {"Music", "Музыка"},
        {"Dash", "Скачок" },
        {"Bag for grenades", "Сумка для гранат" },
        {"Grenade", "Граната" },
        { "Magnet", "Магнит"},
        {"Bugs have a level now: ","У жуков теперь уровень: " },
        {"The boss has appeared!", "Появился босс!" }
    };

    private TextMeshProUGUI[] texts;
    [SerializeField] static public string currentLanguage = "English";
    [SerializeField] private TMP_Dropdown dropdown;

    private void Start()
    {
        currentLanguage = SaveSystem.Data.language;

        texts = GameObject.FindObjectsOfType<TextMeshProUGUI>(true);
        if (currentLanguage == "English") dropdown.value = 0;
        if (currentLanguage == "Russia") dropdown.value = 1;
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
            currentLanguage = "English";
        }
        if (val == 1)
        {
            for (int i = 0; i < texts.Length; i++)
            { 
                if (EnglishToRussiaTranslator.ContainsKey(texts[i].text)) texts[i].text = EnglishToRussiaTranslator[texts[i].text];
            }
            currentLanguage = "Russia";
        }
        SaveSystem.Data.language = currentLanguage;
    }

    public void UpdateLanguage()
    {
        if (currentLanguage == "Russia")
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (EnglishToRussiaTranslator.ContainsKey(texts[i].text)) texts[i].text = EnglishToRussiaTranslator[texts[i].text];
            }
            currentLanguage = "Russia";
        }
        if (currentLanguage == "English")
        {
            for (int i = 0; i < texts.Length; i++)
            {
                if (EnglishToRussiaTranslator.ContainsValue(texts[i].text)) texts[i].text = EnglishToRussiaTranslator.FirstOrDefault(x => x.Value == texts[i].text).Key; ;
            }
            currentLanguage = "English";
        }
        SaveSystem.Data.language = currentLanguage;
    }

    public static string TranslateText(string text)
    {
        string s = text;
        if (currentLanguage == "Russia") s =EnglishToRussiaTranslator[text];
        else s = text;
        return s;
    }
}
